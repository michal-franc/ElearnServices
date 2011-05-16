using System;
using System.Web.Mvc;
using NHiberanteDal.DTO;
using elearn.JsonMessages;
using elearn.CourseService;
using System.Linq;

namespace elearn.Controllers
{
    public class ShoutBoxController : Controller
    {
        private const int NumberOfMessages = 10;
        private readonly ICourseService _courseService;

        public ShoutBoxController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        //
        // Post: /ShoutBox/Add/id?message=test&author=test

        [HttpPost]
        public ActionResult Add(int id,string message)
        {
            int? returnedId = _courseService.AddShoutBoxMessage(new ShoutBoxMessageModelDto
            {
                Author =User.Identity.Name,
                Message = message,
                ShoutBoxId = id,
                TimePosted = DateTime.Now 
            });

            return Json(returnedId.HasValue ? new ResponseMessage(true,String.Empty) : new ResponseMessage(false, String.Empty));
        }

        //
        // Post: /ShoutBox/Get/id
        [HttpPost]
        public ActionResult GetMessages(int id)
        {
            var messages = _courseService.GetLatestShoutBoxMessages(id,NumberOfMessages);
            return Json(messages != null ? new ResponseMessage(true, messages) : new ResponseMessage(false, new object[0]));
        }

    }
}
