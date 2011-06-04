using System;
using System.Web.Mvc;
using NHiberanteDal.DTO;
using elearn.ProfileService;
using elearn.TestService;
using elearn.CourseService;

namespace elearn.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        private readonly ICourseService _courseService;
        private readonly IProfileService _profileService;

        //todo  : add Authorize parameters

        public TestController(ITestService tService, ICourseService cService, IProfileService pService)
        {
            _testService=tService;
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
            ViewBag.TestTypes = new SelectList(testTypes,"ID","TypeName");
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

           test.Author =  _profileService.GetByName(User.Identity.Name);


            if (ModelState.IsValid)
            {
                var newId = _testService.AddTest(35,test);
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


        //todo : Add Question - post action
        //todo : Add Answer - posrt action
    }
}
