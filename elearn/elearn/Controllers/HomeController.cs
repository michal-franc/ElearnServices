using System.Web.Mvc;

namespace elearn.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Welcome()
        {
            return View();
        }

    }
}
