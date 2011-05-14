﻿using System;
using System.Web.Mvc;
using NHiberanteDal.DTO;
using elearn.JsonMessages;
using elearn.CourseService;

namespace elearn.Controllers
{
    public class ShoutBoxController : Controller
    {
        private readonly ICourseService _courseService;

        public ShoutBoxController():this(new CourseServiceClient())
        {
                
        }

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

    }
}
