using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Buking.Context;
using Buking.Enum;
using Buking.Models.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Buking.Controllers
{
    [Authorize]
    public class GuestHousController : Controller
    {
        private DataContext _context;

        public GuestHousController(DataContext dataContext)
        {
            _context = dataContext;
        }

        // GET: GuestHous
        public async Task<ActionResult> Index()
        {
            var currentUser = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserId = currentUser.Id;
            // var currentUserId = _context.Users.SingleOrDefault(u => u.Id == User.Identity.GetUserId());
            return View(await _context.GuestHouses.Where(p => p.CreatedBy.Id == currentUserId).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Index(string name, SearchTypes type)
        {
            if (name != "")
            {
                string searchType = type.ToString();
                if (searchType == "Name")
                {
                    var search = await _context.GuestHouses.Where(x => x.Name.Contains(name)).ToListAsync();
                    if (search.Count == 0)
                        ViewBag.BrakTakiejOsoby = "BRAK OFERTY";

                    return View(search);
                }
                else if (searchType == "Region")
                {
                    var search = await _context.GuestHouses.Where(x => x.Region.Contains(name)).ToListAsync();
                    if (search.Count == 0)
                        ViewBag.BrakTakiejOsoby = "BRAK OFERTY";

                    return View(search);
                }
                else if (searchType == "City")
                {
                    var search = await _context.GuestHouses.Where(x => x.City.Contains(name)).ToListAsync();
                    if (search.Count == 0)
                        ViewBag.BrakTakiejOsoby = "BRAK OFERTY";

                    return View(search);
                }
                else if (searchType == "Street")
                {
                    var search = await _context.GuestHouses.Where(x => x.Street.Contains(name)).ToListAsync();
                    if (search.Count == 0)
                        ViewBag.BrakTakiejOsoby = "BRAK OFERTY";

                    return View(search);
                }
                else
                {
                    return View();
                }
            }

            return View(await _context.GuestHouses.ToListAsync());

        }

        // GET: GuestHous/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuestHouse guestHouse = await _context.GuestHouses.Include(c => c.Bookeds).Where(c => c.Id == id.Value).SingleOrDefaultAsync();

            if (guestHouse == null)
            {
                return HttpNotFound();
            }
            return View(guestHouse);
        }

        // GET: GuestHous/Create
        [Authorize(Roles = "Vendor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: GuestHous/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,City,Street,StreetNr,Region,Price,Path,CreatedBy")] GuestHouse guestHouse, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
                    ;
                    var path = Path.Combine(Server.MapPath("~/Upload/"), fileName);
                    guestHouse.Path = fileName;
                    file.SaveAs(path);
                }
                guestHouse.Id = Guid.NewGuid();
                guestHouse.CreatedBy = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);

                _context.GuestHouses.Add(guestHouse);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(guestHouse);
        }

        // GET: GuestHous/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuestHouse guestHouse = await _context.GuestHouses.FindAsync(id);
            if (guestHouse == null)
            {
                return HttpNotFound();
            }
            return View(guestHouse);
        }

        // POST: GuestHous/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,City,Street,StreetNr,Region")] GuestHouse guestHouse)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(guestHouse).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(guestHouse);
        }

        // GET: GuestHous/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuestHouse guestHouse = await _context.GuestHouses.FindAsync(id);
            if (guestHouse == null)
            {
                return HttpNotFound();
            }
            return View(guestHouse);
        }

        // POST: GuestHous/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            GuestHouse guestHouse = await _context.GuestHouses.FindAsync(id);
            _context.GuestHouses.Remove(guestHouse);
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
