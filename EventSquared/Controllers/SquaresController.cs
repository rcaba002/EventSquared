using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventSquared.Controllers
{
    public class SquaresController : Controller
    {
        // GET: Squares
        public ActionResult All()
        {
            return View();
        }
    }
}