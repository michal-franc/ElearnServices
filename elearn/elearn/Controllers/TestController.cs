using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHiberanteDal.DTO;
using elearn.TestService;
using elearn.CourseService;

namespace elearn.Controllers
{
    public class TestController : Controller
    {
        ITestService _testService;
        ICourseService _courseService;


        public TestController(ITestService _tService,ICourseService _cService)
        {
            _testService=_tService;
            _courseService = _cService;
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
            TestDto test = new TestDto();
            var courses = _courseService.GetAllSignatures();
            ViewData["Courses"] = new SelectList(courses);
            return View(test);
        }

        //
        // GET: /Test/Create
        [HttpPost]
        public ActionResult Create(TestDto test)
        {
            if (ModelState.IsValid)
            {
                _testService.AddTest(1,test);
                return RedirectToAction("Details", new {id=test.ID });
            }
            return View(test);
        }

        //
        // GET: /Test/Edit/id
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // Post: /Test/Edit/id
        [HttpPost]
        public ActionResult Edit(int id,FormCollection formValues)
        {
            return View();
        }

    }
}
