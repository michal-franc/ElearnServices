using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NHiberanteDal.DTO;
using NLog;
using elearn.JsonMessages;
using elearn.ProfileService;
using elearn.TestService;
using elearn.CourseService;

namespace elearn.Controllers
{
    public class TestController : BaseController
    {
        private readonly ITestService _testService;
        private readonly ICourseService _courseService;
        private readonly IProfileService _profileService;

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //todo  : add Authorize parameters


        public TestController(ITestService tService, ICourseService cService, IProfileService pService)
        {
            _testService = tService;
            _courseService = cService;
            _profileService = pService;
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
        public ActionResult Create()
        {
            var test = new TestDto();
            var testTypes = _testService.GetTestTypes();
            ViewBag.TestTypes = new SelectList(testTypes, "ID", "TypeName");
            return View(test);
        }

        //
        // GET: /Test/Create
        [HttpPost]
        public ActionResult Create(TestDto test)
        {

            //Fixing ModelState Valid Error
            if (ModelState.ContainsKey("TestType"))
            {
                var testTypeId = Int32.Parse(ModelState["TestType"].Value.AttemptedValue);
                test.TestType = new TestTypeModelDto() { ID = testTypeId };
                ModelState.Remove("TestType");
            }

            test.Author = _profileService.GetByName(User.Identity.Name);


            if (ModelState.IsValid)
            {
                var newId = _testService.AddTest(35, test);
                if (newId > 0)
                {
                    return RedirectToAction("Details", new { id = newId });
                }
                else
                {
                    //todo log error
                }
            }
            // bug : problem with null TestTypes in View , recreate TestTypes list before sending model to view
            return View(test);
        }

        //
        // GET: /Test/Edit/id
        public ActionResult Edit(int id)
        {
            var test = _testService.GetTestDetails(id);
            var courses = _courseService.GetAllSignatures();
            ViewData["Courses"] = new SelectList(courses);
            return View(test);
        }

        //
        // Post: /Test/Edit/id
        [HttpPost]
        public ActionResult Edit(TestDto test)
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
            var tests = _testService.GetAllTests();
            return View(tests);
        }



        //todotest 
        [HttpGet]
        public ActionResult CreateQuestion(int id)
        {
            if (id > 0)
            {
                var newQuestion = new TestQuestionModelDto();
                ViewBag.TestId = id;

                return PartialView("_CreateQuestionPartial", newQuestion);
            }
            logger.Error(elearn.Common.ErrorMessages.Test.TestIdError);
            ViewBag.Error = elearn.Common.ErrorMessages.Test.TestIdError;
            return PartialView("_Error");
        }

        [HttpPost]
        public JsonResult CreateQuestion(int id, TestQuestionModelDto questionModel)
        {
            var questionId = _testService.AddQuestion(id, questionModel);
            return Json(new ResponseMessage(true, questionId));
        }

        //write test
        [HttpPost]
        public JsonResult AddAnswers(int id, List<TestQuestionAnswerDto> answers)
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
        public ActionResult DoTest(TestDto testModel)
        {
            if (ModelState.IsValid)
            {
                var mark = CalculateMark(testModel);
                return View("Score",mark);
            }
            return View(testModel);
        }


        private double CalculateMark(TestDto test)
        {
            double allQuestions = test.Questions.Count;
            double correctAnswers = test.Questions.Where(q => q.Answers.Any(a => a.IsSelected && a.Correct)).Count();
            return (correctAnswers / allQuestions) * 6;
        }
    }
}
