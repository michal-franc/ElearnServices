using System.Web.Mvc;

namespace elearn.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Tu bedzie jakas powitalna tresc";

            return View("Index");
        }
    }
}
