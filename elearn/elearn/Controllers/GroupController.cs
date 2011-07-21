
using System.Web.Mvc;
using elearn.GroupService;
using elearn.ProfileService;
using elearn.JsonMessages;
using elearn.Models;
using elearn.JournalService;
using elearn.Session;

namespace elearn.Controllers
{
    public class GroupController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IGroupService _groupService;
        private readonly IJournalService _journalService;

        public GroupController(IGroupService groupService,IJournalService journalService )
        {
            _groupService = groupService;
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

        //todotest : modify 
        //todo : add returned error to error static list
        //
        //Post: /Group/Join/id
        [HttpPost]
        public ActionResult Join(int groupId,int courseId)
        {
            logger.Debug("Join groupId = {0} ",groupId);
            var profileId = SessionStateService.SessionState.GetCurrentUserDataFromSession().ProfileId;
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
        public ActionResult Leave(int groupId)
        {
            logger.Debug("Leave groupId = {0} ", groupId);
            var profileId = SessionStateService.SessionState.GetCurrentUserDataFromSession().ProfileId;
            return Json(_groupService.RemoveProfileFromGroup(groupId, profileId) 
                ? new ResponseMessage(true, string.Empty) : 
                 new ResponseMessage(false, string.Empty));
        }
    }
}
