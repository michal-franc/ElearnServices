using System.Linq;
using System.Web.Mvc;
using elearn.JournalService;
using elearn.ProfileService;
using NHiberanteDal.DTO;
using elearn.JsonMessages;

namespace elearn.Controllers
{
    public class JournalController : Controller
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        IJournalService _journalService;
        IProfileService _profileServicel;

        public JournalController(IJournalService jservice, IProfileService profileServicel)
        {
            _journalService = jservice;
            _profileServicel = profileServicel;
        }

        // todo : add test
        public ActionResult Index()
        {
            return RedirectToAction("MyJournal");
        }

        //
        // GET: /Journal/MyJournal
        [HttpGet]
        [Authorize]
        //todo : add tests
        public ActionResult MyJournal()
        {
            var profile = _profileServicel.GetByName(User.Identity.Name);
            if (profile != null)
            {
                if (profile.Journals.Count > 0)
                {
                    //bug : temporary first
                    var journal = profile.Journals.FirstOrDefault();

                    var journalModel = _journalService.GetJournalDetails(journal.ID);
                    return View(journalModel);
                }
                ViewBag.Error = elearn.Common.ErrorMessages.Journal.NoJournals;
                return View("Error");
            }
            ViewBag.Error = elearn.Common.ErrorMessages.Journal.NoProfile;
            return View("Error");
        }

        //todo : test
        //
        // GET: /Journal/Details/id
        [HttpGet]
        public ActionResult Details(int id)
        {
            var journal = _journalService.GetJournalDetails(id);
            if (journal != null)
            {
                return View(journal);
            }
            ViewBag.Error = elearn.Common.ErrorMessages.Journal.NoJournals;
            return View("Error");
        }


        //todo : test
        [HttpGet]
        public ActionResult AddMark(int id)
        {
            logger.Debug("Starting Add Mark [Get] with : journalId {0}",id);
            var markModel = new JournalMarkModelDto();
            ViewBag.JournalId = id;
            return PartialView("_CreateMarkPartial", markModel);
        }

        //todo : test
        [HttpPost]
        public ActionResult AddMark(int id,JournalMarkModelDto model)
        {
            if (ModelState.IsValid)
            {
                logger.Debug("Add Mark [Post] with : journalId {0} , model {1} ", id, model.ToString());
                if (_journalService.AddMark(id, model))
                {
                    logger.Debug("Add Mark [Post] - OK");
                    return RedirectToAction("Details", new {id = id});
                }
                logger.Debug("Add Mark [Post] - Failed");
                return Json(new ResponseMessage(false));
            }
            logger.Debug("Add Mark [Post] - Model State Invalid");
            return Json(new ResponseMessage(false));
        }

        //todo : test
        [HttpPost]
        public ActionResult RemoveMark(int id,int journalId)
        {
            logger.Debug("Remove Mark [Post] with : markdId - {0}", id);
            if (_journalService.RemoveMark(id))
            {
                logger.Debug("Remove Mark [Post] OK");
                return Json(new ResponseMessage(true))  ;
            }
            logger.Debug("Remove Mark [Post] Failed");
            return Json(new ResponseMessage(false));
        }






    }
}
