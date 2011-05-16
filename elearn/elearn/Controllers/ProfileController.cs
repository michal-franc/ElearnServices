using System.Linq;
using System.Web.Mvc;
using elearn.ProfileService;

namespace elearn.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _service;
        
        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        //
        // GET: /Profile/

        [AuthorizeAttributeWcf(Roles = "admin")]
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        //
        // GET: /Profile/Details/id

        [AuthorizeAttributeWcf(Roles = "admin")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var profile = _service.GetProfile(id);
            if(profile!=null)
                return View(profile);
            return View("NotFound");
        }

        //
        // GET: /Profile/List

        [AuthorizeAttributeWcf(Roles = "admin")]
        [HttpGet]
        public ActionResult List()
        {
            var profiles = _service.GetAllProfiles();

            return View(profiles);
        }

        //
        // GET: /Profile/Edit/id

        [AuthorizeAttributeWcf(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var profile = _service.GetProfile(id);
            if (profile != null)
            {
                var roles = _service.GetAllRoles().ToList();
                if (roles.Count <= 0)
                {
                    roles.Insert(0, Common.Strings.NoRoleValue);
                }
                if (roles.Contains(profile.Role))
                {
                    roles.Remove(profile.Role);
                    roles.Insert(0, profile.Role);
                }
                ViewBag.Role = new SelectList(roles);

                return View(profile);
            }
            return View("NotFound");
        }

        //
        // POST: /Profile/Edit/id

        [AuthorizeAttributeWcf(Roles = "admin")]
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
                        ViewBag.Error = Common.ErrorMessages.Profile.RoleUpdateFail;
                        return View(profile);
                    }
                    return RedirectToAction("Details", new { id = profile.ID });
                }
                ViewBag.Error = Common.ErrorMessages.Profile.ProfileUpdateFail;
                return View(profile);
            }
            return View(profile);
        }


        //
        // POST: /Profile/Delete/id

        [AuthorizeAttributeWcf(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_service.SetAsInactive(id))
                ViewBag.Error = Common.ErrorMessages.Profile.SetAsInactiveFailed;

            return RedirectToAction("List");
        }
    }
}
