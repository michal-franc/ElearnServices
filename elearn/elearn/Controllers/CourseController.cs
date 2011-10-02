using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using elearn.LearningMaterialService;
using elearn.ProfileService;
using NHiberanteDal.DTO;
using elearn.CourseService;
using System;
using elearn.JsonMessages;
using elearn.Helpers;

namespace elearn.Controllers
{
    public class CourseController : Controller
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        Stopwatch sw = new Stopwatch();
        readonly ICourseService _courseService;
        private CourseServiceExternal.ICourseService _courseServiceExternal;
        readonly IProfileService _profileService;
        readonly ILearningMaterialService _learningService;
        private static int externalCounter = 0;
        private static int counter = 0;
        private static int redirectedcounter = 0;
        private string locker = "s";

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

        public CourseController(ICourseService courseService, IProfileService profileService, ILearningMaterialService learningService)
        {
            _courseService = courseService;
            _profileService = profileService;
            _learningService = learningService;
            _courseServiceExternal = new CourseServiceExternal.CourseServiceClient();
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
            var course = _courseService.GetById(id);
            return View(course);
        }

        //todo not optimal data retrieving , paging should be on the database side
        // GET: /Course/List/pageNumber
        [HttpGet]
        public ActionResult List()
        {
            counter++;

            if (externalCounter > 0)
            {
                redirectedcounter++;
                Logger.Info("Redirecting to external service");
                var courses = _courseService.GetAllSignatures();

                lock (locker)
                {
                    if (externalCounter > 0)
                    {
                        externalCounter--;

                        Logger.Info("External Counter minus - {0}", externalCounter);
                    }
                }
                Logger.Info("Counter - {0}", redirectedcounter);
                return View(courses);
            }


            else
            {
                sw.Start();
                var courses = _courseServiceExternal.GetAllSignatures();
                sw.Stop();
                if (sw.ElapsedMilliseconds > 100)
                {
                    sw.Reset();
                    lock (locker)
                    {
                        externalCounter++;
                        Logger.Info("External Counter plus - {0}", externalCounter);
                    }
                }
                Logger.Info("Counter - {0}", counter);
                return View(courses);
            }
            return View("Error");
        }

        //todo fix course type logic
        // GET: /Course/Create/
        [HttpGet]
        //[AuthorizeAttributeWcf(Roles = "collaborator")]
        public ActionResult Create()
        {
            var course = new NHiberanteDal.DTO.CourseDto();
            var courseTypes = _courseService.GetAllCourseTypes().ToList();
            ViewBag.CourseTypes = new SelectList(courseTypes, "ID", "TypeName");
            return View(course);
        }

        // Post: /Course/Create/
        [HttpPost]
        //[AuthorizeAttributeWcf(Roles = "collaborator")]
        public ActionResult Create(NHiberanteDal.DTO.CourseDto course)
        {

            course.CreationDate = DateTime.Now;
            course.ShoutBox = new NHiberanteDal.DTO.ShoutboxModelDto();


            course.CourseType = new NHiberanteDal.DTO.CourseTypeModelDto { ID = NHiberanteDal.DTO.CourseDto.DefaultCourseTypeId };

            course.Group = new NHiberanteDal.DTO.GroupModelDto
                               {
                                   GroupName = "DefaultCourseGroupName",
                                   GroupType = new NHiberanteDal.DTO.GroupTypeModelDto { ID = NHiberanteDal.DTO.CourseDto.DefaultGroupTypeId }
                               };

            //Fixing ModelState Valid Error
            if (ModelState.ContainsKey("CourseType"))
            {
                var courseTypeId = Int32.Parse(ModelState["CourseType"].Value.AttemptedValue);
                course.CourseType = new NHiberanteDal.DTO.CourseTypeModelDto { ID = courseTypeId };
                ModelState.Remove("CourseType");
            }

            if (ModelState.IsValid)
            {

                var id = _courseService.AddCourse(course);

                if (id.HasValue)
                    return RedirectToAction("Details", new { id });

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
            if (_courseService.Remove(id))
            {
                return RedirectToAction("List", new { id = 1 });
            }
            ViewBag.Error = Common.ErrorMessages.Course.DeleteError;
            return View("Error");
        }

        //Get: /Course/Edit/id
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var course = _courseService.GetById(id);
            var courseTypes = _courseService.GetAllCourseTypes().ToList();
            var courseTypesList = new SelectList(courseTypes, "ID", "TypeName");
            ViewBag.CourseTypes = courseTypesList;

            return View(course);
        }


        //todo rpzemyslec tutaj logike z course type bo to usuwanie jest bez senus seriously ;/
        //Post: /Course/Edit/id
        [HttpPost]
        public ActionResult Edit(NHiberanteDal.DTO.CourseDto course)
        {
            //Fixing ModelState Valid Error
            if (ModelState.ContainsKey("CourseType"))
            {
                ModelState.Remove("CourseType");
            }

            if (ModelState.IsValid)
            {
                if (_courseService.Update(course, true))
                {
                    return RedirectToAction("Details", new { id = course.ID });
                }

                ViewBag.Error = Common.ErrorMessages.Course.UpdateToDbError;
                return View("Error");
            }

            ViewBag.Error = Common.ErrorMessages.Course.ModelUpdateError;
            return View("Error");
        }


        //Post: /Course/CheckPassword
        [HttpPost]
        public ActionResult CheckPassword(int courseId, string password)
        {
            if (_courseService.CheckPassword(courseId, Md5Hash.EncodePassword(password)))
                return Json(new ResponseMessage(true, String.Empty));
            return Json(new ResponseMessage(false, String.Empty));
        }

        //todotest
        [HttpGet]
        public ActionResult MyCourses()
        {
            var profile = _profileService.GetByName(User.Identity.Name);
            if (profile != null)
            {
                var courses = _courseService.GetCourseSignaturesByProfileId(profile.ID).ToList();
                return View("List", courses);
            }
            ViewBag.Error = Common.ErrorMessages.Profile.NoProfile;
            return View("Error");
        }


        //todotest
        [HttpGet]
        public ActionResult AddLearningMaterial(int id)
        {
            var course = _courseService.GetById(id);
            var newLearningMaterial = new LearningMaterialSignatureDto { CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Title = "New LM", IconName = "sidebar" };
            course.LearningMaterials.Add(newLearningMaterial);
            _courseService.Update(course, false);
            return RedirectToAction("Edit", new { id = id });
        }


        [AuthorizeAttributeWcf(Roles = "admin")]
        [HttpGet]
        public ActionResult Admin()
        {
            var courses = _courseService.GetAllSignatures();
            return View("Admin", courses);
        }
    }
}
