using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using elearn.JournalService;
using NHiberanteDal.DTO;
using NLog;
using elearn.JsonMessages;
using elearn.ProfileService;
using elearn.TestService;
using elearn.CourseService;
using elearn.Models;

namespace elearn.Controllers
{
    public class TestController : BaseController
    {
        private readonly ITestService _testService;
        private readonly ICourseService _courseService;
        private readonly IProfileService _profileService;
        private readonly IJournalService _journalService;

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //todo  : add Authorize parameters


        public TestController(ITestService tService, ICourseService cService, IProfileService pService, IJournalService jService)
        {
            _testService = tService;
            _courseService = cService;
            _profileService = pService;
            _journalService = jService;
        }

        //
        // GET: /Test/

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        //
        // GET: /Test/Details/id
        public ActionResult Details(int id)
        {
            var test = _testService.GetTestDetails(id);
            return View(test);
        }

        //
        // GET: /Test/Create
        [HttpGet]
        public ActionResult Create(int id,int typeId)
        {
            var test = new NHiberanteDal.DTO.TestDto();
            var testTypes = _testService.GetTestTypes();
            test.TestType = testTypes.Where(c => c.ID == typeId).SingleOrDefault();
            ViewBag.ItemId = id;
            return View(test);
        }

        //
        // GET: /Test/Create
        [HttpPost]
        public ActionResult Create(NHiberanteDal.DTO.TestDto test, int itemid)
        {
            test.Author = _profileService.GetByName(User.Identity.Name);
            test.CreationDate = DateTime.Now;
            test.EditDate = DateTime.Now;

            //if (ModelState.IsValid)
            //{
                var newId = 0;
                if (test.TestType.ID == 2)
                    newId = _testService.AddTestToCourse(itemid, test);
                else if(test.TestType.ID == 1)
                    newId = _testService.AddTestToLearningMaterial(itemid, test);
                if (newId > 0)
                {
                    return RedirectToAction("Edit", new { id = newId });
                }
                else
                {
                    //todo log error
                }
            //}
            // bug : problem with null TestTypes in View , recreate TestTypes list before sending model to view
            return View(test);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
           var result = _testService.DeleteTest(id);
           if (result)
               return PartialView("_StatusPartial",new StatusModel("Successfully deleted Test",StatusType.Green));
           else
               return PartialView("_StatusPartial", new StatusModel("Could not delete test", StatusType.Red));
        }

        //
        // GET: /Test/Edit/id
        public ActionResult Edit(int id)
        {
            var test = _testService.GetTestDetails(id);
            return View(test);
        }

        //
        // Post: /Test/Edit/id
        [HttpPost]
        public ActionResult Edit(NHiberanteDal.DTO.TestDto test)
        {
            if (ModelState.IsValid)
            {
                _testService.UpdateTest(test);
                return RedirectToAction("Details", new { id = test.ID });
            }
            return View(test);
        }

        //
        // GET: /Test/List
        [HttpGet]
        public ActionResult List()
        {
            //var tests = _testService.GetNotFinishedTests();
            throw new NotImplementedException();
            //return View(tests);
        }

        //todotest 
        [HttpGet]
        public ActionResult CreateQuestion(int id)
        {
            if (id > 0)
            {
                var newQuestion = new NHiberanteDal.DTO.TestQuestionModelDto();
                ViewBag.TestId = id;
                return PartialView("_CreateQuestionPartial", newQuestion);
            }
            logger.Error(Common.ErrorMessages.Test.TestIdError);
            return PartialView("_ModalError");
        }

        [HttpPost]
        public JsonResult CreateQuestion(int id, NHiberanteDal.DTO.TestQuestionModelDto questionModel)
        {
            var questionId = _testService.AddQuestion(id, questionModel);
            return Json(new ResponseMessage(true, questionId));
        }

        //todotest 
        [HttpGet]
        public ActionResult EditQuestion(int id)
        {
            if (id > 0)
            {
                var question = _testService.GetTestQuestion(id);
                return PartialView("_EditQuestionPartial", question);
            }
            logger.Error(Common.ErrorMessages.Test.TestIdError);
            return PartialView("_ModalError");
        }

        [HttpPost]
        public JsonResult EditQuestion(int id, NHiberanteDal.DTO.TestQuestionModelDto questionModel)
        {
            questionModel.ID = id;
            var result = _testService.UpdateTestQuestion(questionModel);
            return Json(new ResponseMessage(result));
        }


        [HttpPost]
        public JsonResult DeleteQuestion(int id)
        {
            var result = _testService.DeleteTestQuestion(id);
            return Json(new ResponseMessage(result));
        }


        //write test
        [HttpPost]
        public JsonResult AddAnswers(int id, List<NHiberanteDal.DTO.TestQuestionAnswerDto> answers)
        {
            if (_testService.AddAnswers(id, answers.ToArray()))
                return Json(new ResponseMessage(true));
            else
                return Json(new ResponseMessage(false));
        }

        //todo implement Edit Question action

        [HttpGet]
        public ActionResult Finished()
        {
            var profile = _profileService.GetByName(User.Identity.Name);
            if (profile != null)
            {
                return View(profile.FinishedTests.ToList());
            }
            ViewBag.Error = elearn.Common.ErrorMessages.Profile.NoProfile;
            return View("Error");
        }


        [HttpGet]
        public ActionResult SampleTest()
        {
            //sample test id 
            // todo get from config
            var sampleTestId = 25;
            var sampleTest = _testService.GetTestDetails(sampleTestId);
            return View(sampleTest);
        }

        [HttpGet]
        public ActionResult DoTest(int Id)
        {
            var test = _testService.GetTestDetails(Id);
            if (test != null)
            {
                return View(test);
            }
            ViewBag.Error = Common.ErrorMessages.Test.TestIdError;
            return View("Error");
        }

        [HttpPost]
        public ActionResult DoTest(NHiberanteDal.DTO.TestDto testModel)
        {
            if (ModelState.IsValid)
            {
                var mark = CalculateMark(testModel, 100);
                var profile = _profileService.GetByName(User.Identity.Name);

                profile.FinishedTests.Add(new NHiberanteDal.DTO.FinishedTestModelDto()
                                              {
                                                  DateFinished = DateTime.Now,
                                                  Mark = mark,
                                                  TestId = testModel.ID,
                                                  TestName = testModel.Name
                                              });
                var courseId = _journalService.GetCourseIdForTest(testModel.ID);
                var journal = profile.Journals.Where(j => j.Course.ID == courseId).FirstOrDefault();
                if (journal != null)
                {
                    journal.Marks.Add(new NHiberanteDal.DTO.JournalMarkModelDto
                                                {
                                                    DateAdded = DateTime.Now,
                                                    Name = testModel.Name ?? "None",
                                                    Value = mark.ToString()
                                                });
                }
                _profileService.UpdateProfile(profile);
                return View("Score", mark);
            }
            return View(testModel);
        }

        /// <summary>
        /// Method used to calculate final score
        /// </summary>
        /// <param name="test">Instance of test model with questions and answers</param>
        /// <param name="maxValue">Maximum value threshold eg with 100 ( we would had 0-100 mark )</param>
        /// <returns></returns>
        private double CalculateMark(NHiberanteDal.DTO.TestDto test, int maxValue)
        {
            var allQuestionsWithAnswers = test.Questions.Where(q => q.Answers != null).ToList();
            double correctAnswers = allQuestionsWithAnswers.Where(q => q.Answers.Any(a => a.IsSelected && a.Correct)).Count();
            return (correctAnswers / allQuestionsWithAnswers.Count) * maxValue;
        }
    }
}
