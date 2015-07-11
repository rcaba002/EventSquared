using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventSquared.Models;
using Microsoft.AspNet.Identity;

namespace EventSquared.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public ActionResult All()
        {
            var events = db.Events.Include(@event => @event.Address).Include(@event => @event.ApplicationUser);
            return View(events.ToList());
        }

        // GET: Events/new
        public ActionResult New()
        {
            return View();
        }

        // POST: Events/new
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(newViewModel model)
        {
            if (ModelState.IsValid)
            {
                var address = new Address()
                {
                    Street = model.Street,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode
                };

                db.Addresses.Add(address);
                db.SaveChanges();

                var @event = new Event()
                {
                    StartDate = model.StartDate,
                    Title = model.Title,
                    Description = model.Description,
                    ApplicationUserId = User.Identity.GetUserId(),
                    AddressId = address.Id
                };

                db.Events.Add(@event);
                db.SaveChanges();

                return RedirectToAction("All");
            }

            return View(model);
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var @event = db.Events.Include(x => x.Address).SingleOrDefault(x => x.Id == id.Value);

            if (@event == null)
            {
                return HttpNotFound();
            }
            detailsViewModel myViewModel = null;

            if (User.Identity.GetUserId() == @event.ApplicationUserId)
            {
                myViewModel = new detailsViewModel
                {
                    StartDate = @event.StartDate,
                    Title = @event.Title,
                    Description = @event.Description,
                    ApplicationUserId = User.Identity.GetUserId(),
                    AddressId = @event.Address.Id,
                    Street = @event.Address != null ? @event.Address.Street : "",
                    City = @event.Address != null ? @event.Address.City : "",
                    State = @event.Address != null ? @event.Address.State : "",
                    ZipCode = @event.Address != null ? @event.Address.ZipCode : ""
                };
            }
            else
            {
                ViewBag.ErrorMessage = "Only the event organizer can make changes to the event.";
            }

            return View(myViewModel);
        }

        // POST: Events/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(detailsViewModel myViewModel)
        {
            if (ModelState.IsValid)
            {
                var @event = db.Events.FirstOrDefault(x => x.Id == myViewModel.Id);

                @event.Title = myViewModel.Title;
                @event.StartDate = myViewModel.StartDate;
                @event.Description = myViewModel.Description;
                @event.Address.Street = myViewModel.Street;
                @event.Address.City = myViewModel.City;
                @event.Address.State = myViewModel.State;
                @event.Address.ZipCode = myViewModel.ZipCode;

                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();

                // create message telling user changes have been saved

                return RedirectToAction("All");
            }

            return View("All");
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