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
        public ActionResult Join(GroupModelDto groupModel)
        {
            if (groupModel.Users.Any(p => p.Name == User.Identity.Name))
            {
                return PartialView("_AlreadyInGroup");
            }
            return PartialView("_Join",new {groupModel.ID});
        }

        //
        //Get: /Group/Leave/id
        [HttpGet]
        public ActionResult Leave(GroupModelDto groupModel)
        {
            if (groupModel.Users.Any(p => p.Name == User.Identity.Name))
            {
                return PartialView("_Leave", new { groupModel.ID });
            }
            return PartialView("_NotInGroup");
        }


        //
        //Post: /Group/Join/id
        [HttpPost]
        public ActionResult Join(int groupId )
        {
            var profile = _profileService.GetByName(User.Identity.Name);
            if (profile != null)
            {
                if (_groupService.AddProfileToGroup(groupId, profile.ID))
                    return Json(new ResponseMessage(true, string.Empty));
            }
            return Json(new ResponseMessage(false, string.Empty));
        }

        //
        //Post: /Group/Leave/id
        [HttpPost]
        public ActionResult Leave(int groupId)
        {
            var profile = _profileService.GetByName(User.Identity.Name);
            if (profile != null)
            {
                if (_groupService.RemoveProfileFromGroup(groupId, profile.ID))
                    return Json(new ResponseMessage(true, string.Empty));
            }
            return Json(new ResponseMessage(false, string.Empty));
        }

    }
}
