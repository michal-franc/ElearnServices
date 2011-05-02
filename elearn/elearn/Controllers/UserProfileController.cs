using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using elearn.ProfileService;

namespace elearn.Controllers
{
    public class UserProfileController : Controller
    {

        IProfileService _service;

        private string _userName;

        public string UserName
        {
            get
            {
                if (_userName == null)
                    return User.Identity.Name;
                else
                    return _userName;
            }
            set
            {
                _userName = value;
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
            else
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
