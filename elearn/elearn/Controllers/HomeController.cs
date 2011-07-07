using System.Web.Mvc;

namespace elearn.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Tu bedzie jakas powitalna tresc";

            return View("Index");
        }

        public ActionResult Welcome()
        {
            return View();
        }

    }
}
