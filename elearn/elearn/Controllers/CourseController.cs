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
    }
}
