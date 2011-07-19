using System.Web.Mvc;
using elearn.LearningMaterialService;

namespace elearn.Controllers
{
    public class LearningMaterialController : Controller
    {
        private readonly ILearningMaterialService _learningMatService;


        public LearningMaterialController(ILearningMaterialService learningMatservice)
        {
            _learningMatService = learningMatservice;
        }

        public ActionResult Details(int id)
        {
            var learningMaterial = _learningMatService.GetById(id);
            return View(learningMaterial);
        }

        public ActionResult Edit(int id)
        {
            var learningMaterial = _learningMatService.GetById(id);
            return View(learningMaterial);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateGoals(int id,string data)
        {
            var learningMaterial = _learningMatService.GetById(id);
            learningMaterial.Goals = data;
            var ok = _learningMatService.Update(learningMaterial);

            if (ok)
                return Json(true);
            return Json(false);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateDescription(int id,string data)
        {
            var learningMaterial = _learningMatService.GetById(id);
            learningMaterial.Description = data;
            var ok = _learningMatService.Update(learningMaterial);

            if (ok)
                return Json(true);
            return Json(false);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateSummary(int id,string data)
        {
            var learningMaterial = _learningMatService.GetById(id);
            learningMaterial.Summary = data;
            var ok = _learningMatService.Update(learningMaterial);

            if (ok)
                return Json(true);
            return Json(false);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateSection(int id,string data,int sectionNumber)
        {
            var learningMaterial = _learningMatService.GetById(id);
            learningMaterial.Sections[sectionNumber].Text = data;
            var ok = _learningMatService.Update(learningMaterial);

            if (ok)
                return Json(true);
            return Json(false);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateLinks(int id, string data)
        {
            var learningMaterial = _learningMatService.GetById(id);
            learningMaterial.Links = data;
            var ok = _learningMatService.Update(learningMaterial);

            if (ok)
                return Json(true);
            return Json(false);
        }

        [HttpGet]
        public ActionResult AddSections(int id,int count)
        {
            var learningMaterial = _learningMatService.GetById(id);
            for (int i = 0; i < count; i++)
            {
                var newSection = new NHiberanteDal.DTO.SectionDto {Title = "New Section"};
                learningMaterial.Sections.Add(newSection);
            }
            var ok = _learningMatService.Update(learningMaterial);

            return RedirectToAction("Edit");
        }


        [HttpGet]
        public ActionResult RemoveSection(int id, int sectionId)
        {
            var learningMaterial = _learningMatService.GetById(id);

            learningMaterial.Sections.RemoveAt(sectionId);

            var ok = _learningMatService.Update(learningMaterial);

            return RedirectToAction("Edit");
        }

    }
}
