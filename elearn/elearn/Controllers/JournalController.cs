using System;
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

        // todotest
        public ActionResult Index()
        {
            return RedirectToAction("MyJournal");
        }

        //
        // GET: /Journal/MyJournal
        [HttpGet]
        [Authorize]
        //todotest
        public ActionResult MyJournal()
        {
            var profile = _profileServicel.GetByName(User.Identity.Name);
            if (profile != null)
            {
                return View(profile.Journals);
            }
            ViewBag.Error = elearn.Common.ErrorMessages.Journal.NoProfile;
            return View("Error");
        }

        //todotest
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

        //todotest
        //todo : check if course owner
        //
        // GET: /Journal/Edit/id
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var journal = _journalService.GetJournalDetails(id);
            if (journal != null)
            {
                return View(journal);
            }
            ViewBag.Error = elearn.Common.ErrorMessages.Journal.NoJournals;
            return View("Error");
        }


        //todotest
        [HttpGet]
        public ActionResult AddMark(int id)
        {
            logger.Debug("Starting Add Mark [Get] with : journalId {0}",id);
            var markModel = new JournalMarkModelDto();
            ViewBag.JournalId = id;
            return PartialView("_CreateMarkPartial", markModel);
        }

        //todotest
        [HttpPost]
        public ActionResult AddMark(int id,JournalMarkModelDto model)
        {
            if (ModelState.IsValid)
            {
                model.DateAdded = DateTime.Now;

                logger.Debug("Add Mark [Post] with : journalId {0} , model {1} ", id, model.ToString());
                if (_journalService.AddMark(id, model))
                {
                    logger.Debug("Add Mark [Post] - OK");
                    return RedirectToAction("Edit", new {id = id});
                }
                logger.Debug("Add Mark [Post] - Failed");
                return Json(new ResponseMessage(false));
            }
            logger.Debug("Add Mark [Post] - Model State Invalid");
            return Json(new ResponseMessage(false));
        }

        //todotest
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
