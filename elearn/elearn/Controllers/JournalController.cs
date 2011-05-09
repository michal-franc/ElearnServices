using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace elearn.Controllers
{
    public class JournalController : Controller
    {
        //
        // GET: /Journal/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Journal/Details/id
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Journal/Edit/id
        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}
