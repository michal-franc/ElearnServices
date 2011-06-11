
using System.Web.Mvc;
using elearn.GroupService;
using elearn.ProfileService;
using elearn.JsonMessages;
using elearn.Models;

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
        public ActionResult Join(int groupId,int courseId, bool isPasswordProtected)
        {
            var profile = _profileService.GetByName(User.Identity.Name);
            if (profile != null)
            {
                return PartialView("_Join", new JoinGroupModel(profile.ID, groupId,courseId,isPasswordProtected));
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
                return PartialView("_Leave", new ProfileIDGroupIDModel(profile.ID, groupId));
            }
            ViewBag.Error = Common.ErrorMessages.Group.ProfileLeaveError;
            return PartialView("_Error");

        }


        //
        //Post: /Group/Join/id
        [HttpPost]
        public ActionResult Join(int groupId,int profileId)
        {
            return Json(_groupService.AddProfileToGroup(groupId, profileId) 
                ? new ResponseMessage(true, string.Empty) :
                 new ResponseMessage(false, string.Empty));
        }

        //
        //Post: /Group/Leave/id
        [HttpPost]
        public ActionResult Leave(int groupId, int profileId)
        {
            return Json(_groupService.RemoveProfileFromGroup(groupId, profileId) 
                ? new ResponseMessage(true, string.Empty) : 
                 new ResponseMessage(false, string.Empty));
        }


        [HttpGet]
        public ActionResult MyGroups()
        {
            //todo get groups for current user
            return View();
        }
    }
}
