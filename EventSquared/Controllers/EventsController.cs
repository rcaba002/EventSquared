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
            var model = new allEventViewModel
            {
                yourEvents = db.Events.ToList().Where(x => x.ApplicationUserId == User.Identity.GetUserId()),
                yourSquares = db.Squares.ToList().Where(x => x.ApplicationUserId == User.Identity.GetUserId()),
                allSquares = db.Squares.OrderByDescending(x => x.CurrentTime).ToList()
            };

            return View(model);
        }

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

            var myViewModel = new eventDetailsViewModel
            {
                Id = @event.Id,
                StartDate = @event.StartDate,
                Title = @event.Title,
                Description = @event.Description,
                Street = @event.Address != null ? @event.Address.Street : "",
                City = @event.Address != null ? @event.Address.City : "",
                State = @event.Address != null ? @event.Address.State : "",
                ZipCode = @event.Address != null ? @event.Address.ZipCode : "",
                ApplicationUserId = User.Identity.GetUserId(),
                AddressId = @event.Address.Id        
            };

            return View(myViewModel);
        }

        // POST: Events/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(eventDetailsViewModel myViewModel)
        {
            var @event = db.Events.FirstOrDefault(x => x.Id == myViewModel.Id);

            if (User.Identity.GetUserId() == @event.ApplicationUserId)
            {
                if (ModelState.IsValid)
                {
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
            }
            else
            {
                ViewBag.ErrorMessage = "Only the event organizer can make changes to the event.";
            }

            return View(myViewModel);
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

            var myViewModel = new eventDetailsViewModel
            {
                Id = @event.Id,
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

            return View(myViewModel);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(eventDetailsViewModel myViewModel)
        {
            var @event = db.Events.FirstOrDefault(x => x.Id == myViewModel.Id);

            if (ModelState.IsValid)
            {
                @event.Title = myViewModel.Title;
                @event.StartDate = myViewModel.StartDate;
                @event.Description = myViewModel.Description;
                @event.Address.Street = myViewModel.Street;
                @event.Address.City = myViewModel.City;
                @event.Address.State = myViewModel.State;
                @event.Address.ZipCode = myViewModel.ZipCode;

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

            return RedirectToAction("All");
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