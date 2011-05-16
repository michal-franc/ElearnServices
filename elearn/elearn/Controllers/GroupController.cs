using System.Web.Mvc;
using elearn.GroupService;
using elearn.ProfileService;
using elearn.JsonMessages;

namespace elearn.Controllers
{
    public class GroupController : Controller
    {

        private readonly IGroupService _groupService;
        private readonly IProfileService _profileService;

        public GroupController(IGroupService groupService,IProfileService profileService)
        {
            _groupService = groupService;
            _profileService = profileService;
        }

        //
        // Get: /Group/Details/id
        [HttpGet]
        public ActionResult Details(int id)
        {
            var group = _groupService.GetGroup(id);
            return View(group);
        }

        //
        //Post: /Group/Join/id
        [HttpPost]
        public ActionResult Join(int id)
        {
            var profile = _profileService.GetByName(User.Identity.Name);
            if (_groupService.AddProfileToGroup(id, profile.ID))
                return Json(new ResponseMessage(true,string.Empty));
            return Json(new ResponseMessage(false, string.Empty));
        }

        //
        //Post: /Group/Leave/id
        [HttpPost]
        public ActionResult Leave(int id)
        {
            var profile = _profileService.GetByName(User.Identity.Name);
            if (_groupService.RemoveProfileFromGroup(id, profile.ID))
                return Json(new ResponseMessage(true, string.Empty));
            return Json(new ResponseMessage(false, string.Empty));
        }

    }
}
