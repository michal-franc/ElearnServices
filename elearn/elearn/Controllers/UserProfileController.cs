﻿using System.Web.Mvc;
using elearn.ProfileService;
using elearn.Session;

namespace elearn.Controllers
{
    public class UserProfileController : Controller
    {
        readonly IProfileService _service;

        public string UserName
        {
            get
            {
                return User.Identity.Name;
            }
        }

        public UserProfileController(IProfileService service)
        {
            _service = service;
        }

        //
        // GET: /UserProfile/
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Details");
        }

        //
        // GET: /UserProfile/Details/
        [Authorize]
        public ActionResult Details()
        {
            var profile = _service.GetByName(UserName);
            return View(profile);
        }

        //
        // GET: /UserProfile/Delete/

        [Authorize]
        public ActionResult Delete()
        {
            if (!_service.SetAsInactiveByName(UserName))
            {
                return RedirectToAction("Details");
            }
            SessionStateService.SessionState.DeleteCurrentUserSession();
            return RedirectToAction("LogOff", "Account");
        }


        //
        // GET: /UserProfile/Edit/
        [Authorize]
        public ActionResult Edit()
        {
            var profile = _service.GetByName(UserName);
            return View(profile);
        }

        //
        // POST: /UserProfile/Edit/

        [HttpPost]
        public ActionResult Edit(FormCollection formValues)
        {
            var profile = _service.GetByName(UserName);
            if (TryUpdateModel(profile))
            {
                if (_service.UpdateProfile(profile))
                {
                    SessionStateService.SessionState.DeleteCurrentUserSession();
                    return RedirectToAction("Details", new { id = profile.ID });
                }
                ViewData["Error"] = "Problem Updating Profile";
                return View(profile);
            }
            ViewData["Error"] = "Validation Error";
            return View(profile);
        }


    }
}
