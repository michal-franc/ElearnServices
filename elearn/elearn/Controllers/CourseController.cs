using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHiberanteDal.DTO;
using elearn.CourseService;

namespace elearn.Controllers
{
    public class CourseController : Controller
    {
        ICourseService _service;

        private int _limit = -1;

        public int Limit
        {
            get
            {
                if (_limit == -1)
                    _limit = 10;

                return _limit;
            }
            set
            {
                _limit = value;
            }
        }

        public CourseController(ICourseService service)
        {
            _service = service;
        }


        //
        // GET: /Course/

        public ActionResult Index()
        {
            return RedirectToAction("List", new { id = 1 });
        }

        // GET: /Course/Details/id
        public ActionResult Details(int id)
        {
            var course = _service.GetById(id);
            return View(course);
        }


        // GET: /Course/List/id
        public ActionResult List(int id)
        {
            var courses = _service.GetAllSignatures().Skip((id - 1) * Limit).Take(Limit).ToArray();
            return View(courses);
        }

        // GET: /Course/Add/
        public ActionResult Add()
        {
            return View();
        }

        // Post: /Course/Add/
        [HttpPost]
        public ActionResult Add(FormCollection formValues)
        {
            var course = new CourseDto();
            if (TryUpdateModel<CourseDto>(course))
            {
                var id = _service.AddCourse(course);

                if (id > 0)
                    return RedirectToAction("Details", new { id = id });
                else
                {
                    ViewData["Error"] = "Problem in DB while creating Course";
                    return View("Error");
                }
            }
            else
            {
                ViewData["Error"] = "Update model error";
                return View(course);
            }
        }
    }
}
