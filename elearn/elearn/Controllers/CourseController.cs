using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHiberanteDal.DTO;

namespace elearn.Controllers
{
    public class CourseController : Controller
    {
        //
        // GET: /Course/

        public ActionResult Index()
        {
            CourseService.CourseServiceClient client = new CourseService.CourseServiceClient();
            List<CourseSignatureDto> courses = client.GetAllSignatures().ToList();
            client.Close();

            return View(courses);
        }

        // GET: /Course/Details/id
        public ActionResult Details(int id)
        {
            CourseService.CourseServiceClient client = new CourseService.CourseServiceClient();
            CourseDto course = client.GetById(id);
            client.Close();

            return View(course);
        }

        // GET: /Course/Tests/id
        public ActionResult Tests(int id)
        {
            CourseService.CourseServiceClient client = new CourseService.CourseServiceClient();
            var tests = client.GetAllTestsSignatures(id);
            return View(tests);
        }

        // GET: /Course/Surveys/id
        public ActionResult Surveys(int id)
        {
            return View();
        }

    }
}
