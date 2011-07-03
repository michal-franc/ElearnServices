using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace elearn.Controllers
{
    public class InfoController : Controller
    {

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
