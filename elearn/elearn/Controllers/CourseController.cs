using System.Linq;
using System.Web.Mvc;
using NHiberanteDal.DTO;
using elearn.CourseService;

namespace elearn.Controllers
{
    public class CourseController : Controller
    {
        readonly ICourseService _service;

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
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List", new { id = 1 });
        }

        // GET: /Course/Details/id
        [HttpGet]
        public ActionResult Details(int id)
        {
            var course = _service.GetById(id);
            return View(course);
        }


        // GET: /Course/List/id
        [HttpGet]
        public ActionResult List(int id)
        {
            var courses = _service.GetAllSignatures().Skip((id - 1) * Limit).Take(Limit).ToArray();
            return View(courses);
        }

        // GET: /Course/Add/
        [HttpGet]
        public ActionResult Create()
        {
            var course = new CourseDto();
            var courseTypes = _service.GetAllCourseTypes().ToList();
            if (courseTypes.Count <= 0)
            {
                courseTypes.Add(new CourseTypeModelDto{TypeName = "None"});
            }
            ViewBag.CourseType = new SelectList(courseTypes);
            return View(course);
        }

        // Post: /Course/Create/
        [HttpPost]
        public ActionResult Create(CourseDto course)
        {
            if (ModelState.IsValid)
            {
                var id = _service.AddCourse(course);

                if (id > 0)
                    return RedirectToAction("Details", new { id = id });
                else
                {
                    ViewBag.Error = Common.ErrorMessages.Course.AddToDbError;
                    return View("Error");
                }
            }
            else
            {
                ViewBag.Error = Common.ErrorMessages.Course.ModelUpdateError;
                return View("Error");
            }
        }

        // GET: /Course/Delete/id
        public ActionResult Delete()
        {
            return View();
        }

        // Post: /Course/Delete/id
        [HttpPost]
        public ActionResult Delete(int id,FormCollection formValues)
        {
            if (_service.Remove(id))
            {
                return RedirectToAction("List");
            }
            else
            {
                ViewData["Error"] = "Problem Deleting Course";
                return View("Error");
            }
        }

        //Get: /Course/Edit/id
        public ActionResult Edit(int id)
        {
            var course = _service.GetById(id);
            return View(course);
        }

        //Post: /Course/Edit/id
        [HttpPost]
        public ActionResult Edit(CourseDto course)
        {
            if (ModelState.IsValid)
            {
                if (_service.Update(course))
                {
                    return RedirectToAction("Details", new { id = course.ID });
                }
                else
                {
                    ViewBag.Error = Common.ErrorMessages.Course.UpdateToDbError;
                    return View("Error");
                }
            }
            else
            {
                ViewBag.Error = Common.ErrorMessages.Course.ModelUpdateError;
                return View();
            }
        }
    }
}
