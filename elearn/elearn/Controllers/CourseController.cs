using System.Linq;
using System.Web.Mvc;
using NHiberanteDal.DTO;
using elearn.CourseService;
using System;
using elearn.JsonMessages;
using elearn.Helpers;

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


        // GET: /Course/Create/
        [HttpGet]
        //[AuthorizeAttributeWcf(Roles = "collaborator")]
        public ActionResult Create()
        {
            var course = new CourseDto();
            var courseTypes = _service.GetAllCourseTypes().ToList();
            ViewBag.CourseTypes = new SelectList(courseTypes,"ID","TypeName");
            return View(course);
        }

        // Post: /Course/Create/
        [HttpPost]
        //[AuthorizeAttributeWcf(Roles = "collaborator")]
        public ActionResult Create(CourseDto course)
        {

            course.CreationDate = DateTime.Now;
            course.ShoutBox = new ShoutboxModelDto();
            course.Forum = new ForumModelDto
                               {
                Author = "DefaultForumAuthorName",
                Name = "DefaultCourseForumName"
            };

            course.CourseType = new CourseTypeModelDto { ID = CourseDto.DefaultCourseTypeId };

            course.Group = new GroupModelDto
                               {
                GroupName = "DefaultCourseGroupName",
                GroupType = new GroupTypeModelDto { ID = CourseDto.DefaultGroupTypeId }
            };

            //Fixing ModelState Valid Error
            if (ModelState.ContainsKey("CourseType"))
            {
                var courseTypeId = Int32.Parse(ModelState["CourseType"].Value.AttemptedValue);
                course.CourseType = new CourseTypeModelDto { ID = courseTypeId };
                ModelState.Remove("CourseType");
            }

            if (ModelState.IsValid)
            {

                var id = _service.AddCourse(course);

                if (id.HasValue)
                    return RedirectToAction("Details", new {id});
                
                ViewBag.Error = Common.ErrorMessages.Course.AddToDbError;
                return View("Error");
            }
            
            ViewBag.Error = Common.ErrorMessages.Course.ModelUpdateError;
            return View("Error");
        }

        // GET: /Course/Delete/id
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(id);
        }

        //Get: /Course/Delete/id
        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            if (_service.Remove(id))
            {
                return RedirectToAction("List",new{id=1});
            }
            ViewBag.Error = Common.ErrorMessages.Course.DeleteError;
            return View("Error");
        }

        //Get: /Course/Edit/id
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var course = _service.GetById(id);
            var courseTypes = _service.GetAllCourseTypes().ToList();
            ViewBag.CourseTypes = new SelectList(courseTypes, "ID", "TypeName");
            return View(course);
        }

        //Post: /Course/Edit/id
        [HttpPost]
        public ActionResult Edit(CourseDto course)
        {
            //Fixing ModelState Valid Error
            if (ModelState.ContainsKey("CourseType"))
            {
                int courseTypeId = Int32.Parse(ModelState["CourseType"].Value.AttemptedValue);
                course.CourseType = new CourseTypeModelDto { ID = courseTypeId };
                ModelState.Remove("CourseType");
            }

            if (ModelState.IsValid)
            {
                if (_service.Update(course))
                {
                    return RedirectToAction("Details", new { id = course.ID });
                }
                
                ViewBag.Error = Common.ErrorMessages.Course.UpdateToDbError;
                return View("Error");
            }
            
            ViewBag.Error = Common.ErrorMessages.Course.ModelUpdateError;
            return View();
        }


        //Post: /Course/CheckPassword
        [HttpPost]
        public ActionResult CheckPassword(int courseId,string password)
        {
            if (_service.CheckPassword(courseId, Md5Hash.EncodePassword(password)))
                return Json(new ResponseMessage(true, String.Empty));
            return Json(new ResponseMessage(false, String.Empty));
        }
    }
}
