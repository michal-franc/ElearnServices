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

        [HttpPost]
        public JsonResult AddMark(int id,JournalMarkModelDto model)
        {
            if (_journalService.AddMark(id, model))
            {
                return Json(new ResponseMessage(true));
            }
            return Json(new ResponseMessage(false));
        }

        [HttpPost]
        public JsonResult RemoveMark(int id)
        {
            if (_journalService.RemoveMark(id))
            {
                return Json(new ResponseMessage(true));
            }
            return Json(new ResponseMessage(false));
        }






    }
}
