using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHiberanteDal.Models;

namespace elearn.Controllers
{
    public class ProfileController : Controller
    {
        ProfileService.ProfileServiceClient service = new ProfileService.ProfileServiceClient();

        //
        // GET: /Profile/

        [Authorize]
        public ActionResult Index()
        {
            service.Open();
            var profile = service.GetByName(User.Identity.Name);
            service.Close();

            return RedirectToAction("Details", new { id = profile.ID });
        }

        //
        // GET: /Profile/Details/id

        [Authorize]
        public ActionResult Details(int id)
        {
            service.Open();
            var profile = service.GetProfile(id);
            service.Close();

            return View(profile);
        }

        //
        // GET: /Profile/List
            
        [Authorize]
        public ActionResult List()
        {
            service.Open();
            var profiles = service.GetAllProfiles();
            service.Close();

            return View(profiles);
        }

    }
}
