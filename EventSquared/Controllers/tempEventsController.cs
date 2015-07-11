using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventSquared.Models;

namespace EventSquared.Controllers
{
    public class tempEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: tempEvents
        public ActionResult Index()
        {
            var events = db.Events.Include(@event => @event.Address).Include(@event => @event.ApplicationUser);
            return View(events.ToList());
        }

        // GET: tempEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: tempEvents/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Street");
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: tempEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,StartDate,Description,AddressId,ApplicationUserId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Street", @event.AddressId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", @event.ApplicationUserId);
            return View(@event);
        }

        // GET: tempEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Street", @event.AddressId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", @event.ApplicationUserId);
            return View(@event);
        }

        // POST: tempEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,StartDate,Description,AddressId,ApplicationUserId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Street", @event.AddressId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", @event.ApplicationUserId);
            return View(@event);
        }

        // GET: tempEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: tempEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
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
