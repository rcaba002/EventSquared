using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventSquared.Models;

namespace EventSquared.Controllers
{
    public class SquaresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Squares
        public async Task<ActionResult> Index()
        {
            var squares = db.Squares.Include(s => s.Event);
            return View(await squares.ToListAsync());
        }

        // GET: Squares/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Square square = await db.Squares.FindAsync(id);
            if (square == null)
            {
                return HttpNotFound();
            }
            return View(square);
        }

        // GET: Squares/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title");
            return View();
        }

        // POST: Squares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,StartTime,EndTime,Location,Description,EventId")] Square square)
        {
            if (ModelState.IsValid)
            {
                db.Squares.Add(square);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", square.EventId);
            return View(square);
        }

        // GET: Squares/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Square square = await db.Squares.FindAsync(id);
            if (square == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", square.EventId);
            return View(square);
        }

        // POST: Squares/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,StartTime,EndTime,Location,Description,EventId")] Square square)
        {
            if (ModelState.IsValid)
            {
                db.Entry(square).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", square.EventId);
            return View(square);
        }

        // GET: Squares/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Square square = await db.Squares.FindAsync(id);
            if (square == null)
            {
                return HttpNotFound();
            }
            return View(square);
        }

        // POST: Squares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Square square = await db.Squares.FindAsync(id);
            db.Squares.Remove(square);
            await db.SaveChangesAsync();
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
