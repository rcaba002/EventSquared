using EventSquared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace EventSquared.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dashboard
        public ActionResult home()
        {
            var model = new allEventViewModel
            {
                yourEvents = db.Events.ToList().Where(x => x.ApplicationUserId == User.Identity.GetUserId()),
                subscribedEvents = db.Events.ToList().Where(x => x.ApplicationUserId != User.Identity.GetUserId()),
                allSquares = db.Squares.OrderByDescending(x => x.CurrentTime).ToList()
            };

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}