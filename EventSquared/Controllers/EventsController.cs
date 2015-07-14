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

        // GET: Events/new
        public ActionResult New()
        {
            return View();
        }

        // POST: Events/new
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(newEventViewModel model)
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

                return RedirectToAction("home", "Dashboard", "Dashboard");
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

            var model = new detailsEventViewModel
            {
                Id = @event.Id,
                StartDate = @event.StartDate,
                Title = @event.Title,
                Description = @event.Description,
                Street = @event.Address.Street,
                City = @event.Address.City,
                State = @event.Address.State,
                ZipCode = @event.Address.ZipCode,
                ApplicationUserId = @event.ApplicationUserId,
                AddressId = @event.Address.Id        
            };

            return View(model);
        }

        // POST: Events/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(detailsEventViewModel model)
        {
            var @event = db.Events.FirstOrDefault(x => x.Id == model.Id);

            if (User.Identity.GetUserId() == @event.ApplicationUserId)
            {
                if (ModelState.IsValid)
                {
                    @event.Title = model.Title;
                    @event.StartDate = model.StartDate;
                    @event.Description = model.Description;
                    @event.Address.Street = model.Street;
                    @event.Address.City = model.City;
                    @event.Address.State = model.State;
                    @event.Address.ZipCode = model.ZipCode;

                    db.Entry(@event).State = EntityState.Modified;
                    db.SaveChanges();

                    // create message telling user changes have been saved

                    return RedirectToAction("home", "Dashboard", "Dashboard");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Only the event organizer can make changes to the event.";
            }

            return View(model);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
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

            var myViewModel = new detailsEventViewModel
            {
                Id = @event.Id,
                StartDate = @event.StartDate,
                Title = @event.Title,
                Description = @event.Description,
                ApplicationUserId = User.Identity.GetUserId(),
                AddressId = @event.Address.Id,
                Street = @event.Address.Street,
                City = @event.Address.City,
                State = @event.Address.State,
                ZipCode = @event.Address.ZipCode
            };

            return View(myViewModel);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(detailsEventViewModel model)
        {
            var @event = db.Events.FirstOrDefault(x => x.Id == model.Id);

            if (ModelState.IsValid)
            {
                @event.Title = model.Title;
                @event.StartDate = model.StartDate;
                @event.Description = model.Description;
                @event.Address.Street = model.Street;
                @event.Address.City = model.City;
                @event.Address.State = model.State;
                @event.Address.ZipCode = model.ZipCode;

                if (User.Identity.GetUserId() == @event.ApplicationUserId)
                {
                    db.Entry(@event).State = EntityState.Deleted;
                    db.SaveChanges();

                    // create message telling user changes have been saved
                }
                else
                {
                    ViewBag.noDelete = "Only the event organizer can delete the event.";
                }
            }

            return RedirectToAction("home", "Dashboard", "Dashboard");
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