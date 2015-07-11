using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventSquared.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult home()
        {
            return View();
        }
    }
}