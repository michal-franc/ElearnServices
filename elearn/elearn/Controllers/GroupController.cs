
using System.Web.Mvc;
using elearn.GroupService;
using elearn.ProfileService;
using elearn.JsonMessages;
using elearn.Models;
using elearn.JournalService;

namespace elearn.Controllers
{
    public class GroupController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IGroupService _groupService;
        private readonly IProfileService _profileService;
        private readonly IJournalService _journalService;

        public GroupController(IGroupService groupService,IProfileService profileService,IJournalService journalService )
        {
            _groupService = groupService;
            _profileService = profileService;
            _journalService = journalService;
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



        //todotest : modify 
        //todo : add returned error to error static list
        //
        //Post: /Group/Join/id
        [HttpPost]
        public ActionResult Join(int groupId,int profileId,int courseId)
        {
            logger.Debug("Group Controller Action - Join groupId = {0} , profileId = {1}",groupId,profileId);
            if(_groupService.AddProfileToGroup(groupId, profileId))
            {
                if (_journalService.CreateJournal(courseId, profileId))
                {
                    logger.Debug("Group Controller Action - Join  passed");
                    return Json(new ResponseMessage(true, string.Empty));
                }
            }
            logger.Debug("Group Controller Action - Join  failed");
            return Json(new ResponseMessage(false, "Problem adding user to group"));
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
    }
}
