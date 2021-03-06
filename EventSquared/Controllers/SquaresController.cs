﻿using System;
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
    public class SquaresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Squares
        public ActionResult All()
        {
            var model = new allViewModel
            {
                yourSquares = db.Squares.ToList().Where(x => x.ApplicationUserId == User.Identity.GetUserId()),
                allSquares = db.Squares.OrderByDescending(x => x.CurrentTime).ToList()
            };

            return View(model);
        }

        // GET: Squares/new
        public ActionResult New()
        {
            var UserId = User.Identity.GetUserId();

            ViewBag.EventId = new SelectList(db.Events.Where(x => x.ApplicationUserId == UserId), "Id", "Title");
            return View();
        }

        // POST: Squares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([Bind(Include = "Id,Title,StartTime,EndTime,Location,Description,EventId, ApplicationUserId")] Square square)
        {
            var UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                square.CurrentTime = DateTime.Now;
                square.ApplicationUserId = User.Identity.GetUserId();
                db.Squares.Add(square);
                db.SaveChanges();

                return RedirectToAction("All");
            }

            ViewBag.EventId = new SelectList(db.Events.Where(x => x.ApplicationUserId == UserId), "Id", "Title", square.EventId);
            return View(square);
        }

        // GET: Squares/Details/5
        public ActionResult Details(int? id)
        {
            var UserId = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Square square = db.Squares.Find(id);
            if (square == null)
            {
                return HttpNotFound();
            }

            ViewBag.EventId = new SelectList(db.Events.Where(x => x.ApplicationUserId == UserId), "Id", "Title", square.EventId);
            return View(square);
        }

        // POST: Squares/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "Id,Title,StartTime,EndTime,Location,Description,EventId, ApplicationUserId")] Square square)
        {
            var UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                square.CurrentTime = DateTime.Now;
                db.Entry(square).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("All");
            }

            ViewBag.EventId = new SelectList(db.Events.Where(x => x.ApplicationUserId == UserId), "Id", "Title", square.EventId);
            return View(square);
        }

        // GET: Squares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Square square = db.Squares.Find(id);
            if (square == null)
            {
                return HttpNotFound();
            }
            return View(square);
        }

        // POST: Squares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Square square = db.Squares.Find(id);
            db.Squares.Remove(square);
            db.SaveChanges();
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
