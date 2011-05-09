using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace elearn.Controllers
{
    public class SurveyController : Controller
    {
        //
        // GET: /Survey/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Survey/Details/id
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Survey/Edit/id
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // GET: /Survey/Edit/id
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}
