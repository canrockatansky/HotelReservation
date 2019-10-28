using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotels302.Models;

namespace Hotels302.Controllers
{
    [Authorize]
    public class HotelsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Hotels
        public ActionResult Index()
        {
            var hotels = db.Hotels.Include(h => h.City).Include(h => h.Town);
            return View(hotels.ToList());

        }

        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotels/Create
        public ActionResult Create()
        {
            var hotel = new Hotel();

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name");
            return View(hotel);
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HotelName,Address,CityId,TownId,Explanation,RoomCapacity,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] Hotel hotel, IEnumerable<HttpPostedFileBase> ResimDosya)
        {
            if (ModelState.IsValid)
            {
                if (ResimDosya != null)
                {
                    foreach (var item in ResimDosya)
                    {
                        item.SaveAs(Server.MapPath($"/Content/images/{item.FileName}"));
                        hotel.Foto = "/Content/images/" + item.FileName;
                    }
                }

                hotel.CreateDate = DateTime.Now;
                hotel.CreatedBy = "Unknown";
                hotel.UpdateDate = DateTime.Now;
                hotel.UpdatedBy = "Unknown";
                db.Hotels.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
              
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", hotel.CityId);
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", hotel.TownId);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", hotel.CityId);
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", hotel.TownId);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HotelName,Address,CityId,TownId,Explanation,RoomCapacity,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                hotel.UpdateDate = DateTime.Now;
                hotel.UpdatedBy = "Unknown";
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", hotel.CityId);
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", hotel.TownId);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            db.Hotels.Remove(hotel);
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
