using System.Linq;
using System.Web.Mvc;
using elearn.GroupService;
using elearn.ProfileService;
using elearn.JsonMessages;
using NHiberanteDal.DTO;

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
        //Get: /Group/Join/id
        [HttpGet]
        public ActionResult Join(int groupId)
        {
            var profile = _profileService.GetByName(User.Identity.Name);
            if (profile != null)
            {
                return PartialView("_Join", new {groupId, profileId = profile.ID});
            }
            ViewBag.Error = Common.ErrorMessages.Group.ProfileJoinError;
            return PartialView("_Error");
        }

        //
        //Get: /Group/Leave/id
        [HttpGet]
        public ActionResult Leave(int groupId)
        {
            var profile = _profileService.GetByName(User.Identity.Name);
            if (profile != null)
            {
                return PartialView("_Leave", new {groupId, profileId = profile.ID});
            }
            ViewBag.Error = Common.ErrorMessages.Group.ProfileLeaveError;
            return PartialView("_Error");

        }


        //
        //Post: /Group/Join/id
        [HttpPost]
        public ActionResult Join(int groupId,int profileId)
        {
            if (_groupService.AddProfileToGroup(groupId, profileId))
                return Json(new ResponseMessage(true, string.Empty));
             return Json(new ResponseMessage(false, string.Empty));
        }

        //
        //Post: /Group/Leave/id
        [HttpPost]
        public ActionResult Leave(int groupId, int profileID)
        {
            if (_groupService.RemoveProfileFromGroup(groupId, profileID))
                return Json(new ResponseMessage(true, string.Empty));
            return Json(new ResponseMessage(false, string.Empty));
        }

    }
}
