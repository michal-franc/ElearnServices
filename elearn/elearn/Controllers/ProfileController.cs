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

        [Authorize]
        public ActionResult Index()
        {
            var profile = _service.GetByName(UserName);

            return RedirectToAction("Details", new { id = profile.ID });
        }

        //
        // GET: /Profile/Details/id

        [Authorize]
        public ActionResult Details(int id)
        {
            var profile = _service.GetProfile(id);

            return View(profile);
        }

        //
        // GET: /Profile/List
            
        [Authorize]
        public ActionResult List()
        {
            var profiles = _service.GetAllProfiles();

            return View(profiles);
        }

        //
        // GET: /Profile/Edit/id

        [Authorize]
        public ActionResult Edit(int id)
        {
            var profile = _service.GetProfile(id);
            return View(profile);
        }

        //
        // POST: /Profile/Edit/id

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id,FormCollection formValues)
        {
            var profile = _service.GetProfile(id);
            if (TryUpdateModel(profile))
            {
                _service.UpdateProfile(profile);
                return RedirectToAction("Details", new { id = profile.ID });
            }
            return View(profile);
        }


        //
        // GET: /Profile/Delete/id

        [Authorize]
        public ActionResult Delete(int id)
        {
            var profile = _service.GetProfile(id);
            return View(profile);
        }

    }
}
