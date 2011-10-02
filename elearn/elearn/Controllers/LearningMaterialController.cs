using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using elearn.LearningMaterialService;
using elearn.JsonMessages;

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
            var regex = new Regex("<script((.|\n)*)script>", RegexOptions.ExplicitCapture | RegexOptions.Multiline);
            foreach (var sect in learningMaterial.Sections)
            {
                if (sect.Text != null)
                {
                    sect.Text = regex.Replace(sect.Text, "");
                }
            }
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
            var result = _learningMatService.Update(learningMaterial);
            return Json(new ResponseMessage(result));
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateDescription(int id,string data)
        {
            var learningMaterial = _learningMatService.GetById(id);
            learningMaterial.Description = data;
            var result = _learningMatService.Update(learningMaterial);
            return Json(new ResponseMessage(result));
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateSummary(int id,string data)
        {
            var learningMaterial = _learningMatService.GetById(id);
            learningMaterial.Summary = data;
            var result = _learningMatService.Update(learningMaterial);
            return Json(new ResponseMessage(result));
        }


        [HttpPost]
        public ActionResult UpdateInfo(int id,string title, string iconName,int level)
        {
            var learningMaterial = _learningMatService.GetById(id);
            learningMaterial.Title = title;
            learningMaterial.IconName = iconName;
            learningMaterial.Level = level;
            var result = _learningMatService.Update(learningMaterial);
            return Json(new ResponseMessage(result));
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateSection(int id,string data,int sectionNumber,string title)
        {
            var learningMaterial = _learningMatService.GetById(id);
            learningMaterial.Sections.Where(s=>s.ID == sectionNumber).Single().Text = data;
            learningMaterial.Sections.Where(s => s.ID == sectionNumber).Single().Title = title;
            var result = _learningMatService.Update(learningMaterial);
            return Json(new ResponseMessage(result));
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateLinks(int id, string data)
        {
            var learningMaterial = _learningMatService.GetById(id);
            learningMaterial.Links = data;
            var result = _learningMatService.Update(learningMaterial);
            return Json(new ResponseMessage(result));
        }

        [HttpPost]
        public ActionResult AddSections(int id, int count)
        {
            var learningMaterial = _learningMatService.GetById(id);
            for (int i = 0; i < count; i++)
            {
                var newSection = new NHiberanteDal.DTO.SectionDto {Title = "New Section"};
                learningMaterial.Sections.Add(newSection);
            }
            var ok = _learningMatService.Update(learningMaterial);
            return Json(new ResponseMessage(ok));
        }



        [HttpPost]
        public ActionResult RemoveSection(int id, int sectionId)
        {
            var learningMaterial = _learningMatService.GetById(id);

            learningMaterial.Sections.RemoveAt(sectionId);

            var ok = _learningMatService.Update(learningMaterial);
            return Json(new ResponseMessage(ok));
        }

    }
}
