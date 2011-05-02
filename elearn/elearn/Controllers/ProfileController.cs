using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHiberanteDal.Models;
using elearn.ProfileService;

namespace elearn.Controllers
{
    public class ProfileController : Controller
    {
        private IProfileService _service;

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
        
        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        //
        // GET: /Profile/

        [AuthorizeAttributeWCF(Roles = "admin")]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        //
        // GET: /Profile/Details/id

        [AuthorizeAttributeWCF(Roles = "admin")]
        public ActionResult Details(int id)
        {
            var profile = _service.GetProfile(id);
            if(profile!=null)
                return View(profile);
            else
                return View("NotFound");
        }

        //
        // GET: /Profile/List

        [AuthorizeAttributeWCF(Roles = "admin")]
        public ActionResult List()
        {
            var profiles = _service.GetAllProfiles();

            return View(profiles);
        }

        //
        // GET: /Profile/Edit/id

        [AuthorizeAttributeWCF(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var profile = _service.GetProfile(id);
            if (profile != null)
                return View(profile);
            else
                return View("NotFound");
        }

        //
        // POST: /Profile/Edit/id

        [AuthorizeAttributeWCF(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(int id,FormCollection formValues)
        {
            var profile = _service.GetProfile(id);
            if (TryUpdateModel(profile))
            {
                if (_service.UpdateRole(profile, true))
                {
                    if (_service.UpdateProfile(profile))
                    {
                        return RedirectToAction("Details", new { id = profile.ID });
                    }
                    ViewData["Error"] = "Problem Updating Profile";
                    return View(profile);
                }
                ViewData["Error"] = "Problem Updating Role";
                return View(profile);
            }
            ViewData["Error"] = "Validation Error";
            return View(profile);
        }


        //
        // GET: /Profile/Delete/id

        [AuthorizeAttributeWCF(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            if (!_service.SetAsInactive(id))
                ViewData["Error"] = "Problem updating profile";

            return RedirectToAction("List");
        }
    }
}
