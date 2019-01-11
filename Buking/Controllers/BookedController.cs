using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Buking.Context;
using Buking.Models.Database;

namespace Buking.Controllers
{
    
    public class BookedController : Controller
    {
        private DataContext _context;

        public BookedController(DataContext dataContext)
        {
            _context = dataContext;
        }
        // GET: Booked
        public async Task<ActionResult> Index()
        {
            var currentUser = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserId = currentUser.Id;
            return View(await _context.Bookeds.Where(p => p.CreatedBy.Id == currentUserId).ToListAsync());
            //var bookeds = _context.Bookeds.Include(b => b.Id);
           // return View(await bookeds.ToListAsync());
        }

        // GET: Booked/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booked booked = await _context.Bookeds.Include(c=>c.GuestHouse).SingleOrDefaultAsync(c=>c.Id == id.Value);
            if (booked == null)
            {
                return HttpNotFound();
            }
            return View(booked);
        }

        // GET: Booked/Create
        [Authorize(Roles = "User")]
        public ActionResult Create(Guid? id)
        {
            var c = _context.GuestHouses.SingleOrDefault(p => p.Id == id);
            
            var book  = new Booked
            {
                DateStart = DateTime.Now,
                DateStop = DateTime.Now.AddDays(2),
               // Content = "Nowa Rezerwacja",
                GuestHouse_Id = c.Id,
                //CreatedBy = c.CreatedBy,
                BookedBy = _context.Users.SingleOrDefault(u=>u.UserName == User.Identity.Name)
            };
            //ViewBag.GuestHouse_Id = new SelectList(_context.GuestHouses, "Id", "Name");
            return View(book);

        }

        // POST: Booked/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DateStart,DateStop,GuestHouse_Id,CreatedBy, BookedBy")] Booked booked)
        {
            if (ModelState.IsValid)
            {
                var strat = _context.Bookeds.Where(p => p.DateStart >= booked.DateStart).ToList();
                var stop = _context.Bookeds.Where(p => p.DateStop >= booked.DateStop).ToList();
                //if (stop.Count != 0 || strat.Count !=0)
                //{
                //    return RedirectToAction("Index", "GuestHous", booked);
                //}
                
                booked.CreatedBy = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                booked.Id = Guid.NewGuid();
               // booked.GuestHouse = _context.GuestHouses.SingleOrDefault(p => p.Id);
                _context.Bookeds.Add(booked);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GuestHouse_Id = new SelectList(_context.GuestHouses, "Id", "Name", booked.GuestHouse_Id);
            return View(booked);
        }

        // GET: Booked/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booked booked = await _context.Bookeds.FindAsync(id);
            if (booked == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuestHouse_Id = new SelectList(_context.GuestHouses, "Id", "Name", booked.GuestHouse_Id);
            return View(booked);
        }

        // POST: Booked/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DateStart,DateStop,GuestHouse_Id")] Booked booked)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(booked).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GuestHouse_Id = new SelectList(_context.GuestHouses, "Id", "Name", booked.GuestHouse_Id);
            return View(booked);
        }

        // GET: Booked/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booked booked = await _context.Bookeds.FindAsync(id);
            if (booked == null)
            {
                return HttpNotFound();
            }
            return View(booked);
        }

        // POST: Booked/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Booked booked = await _context.Bookeds.FindAsync(id);
            _context.Bookeds.Remove(booked);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
