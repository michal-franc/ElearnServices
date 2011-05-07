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
            {
                var roles = _service.GetAllRoles();
                var list = roles.ToList();
                list.Insert(0,"----");
                if (list.Contains(profile.Role))
                {
                    list.Remove(profile.Role);
                    list.Insert(0, profile.Role);
                }
                ViewData["Role"] = new SelectList(list);

                return View(profile);
            }
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
                if (_service.UpdateProfile(profile))
                {
                    if (profile.Role != "----")
                    {
                        if (_service.UpdateRole(profile, true))
                        {
                            return RedirectToAction("Details", new { id = profile.ID });
                        }
                        ViewData["Error"] = "Problem Updating Role";
                        return View(profile);
                    }
                    else
                    {
                        return RedirectToAction("Details", new { id = profile.ID });
                    }
                }
                ViewData["Error"] = "Problem Updating Profile";
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
