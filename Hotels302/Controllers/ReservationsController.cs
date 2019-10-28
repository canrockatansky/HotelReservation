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
    public class ReservationsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Contact).Include(r => r.Hotel).Include(r => r.RoomType);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create(int id, string hotel)
        {
            
            var reservation = new Reservation();
            if (hotel!="" && hotel!=null)
            {
                ViewBag.HotelName = hotel;
                ViewBag.HotelId = db.Hotels.First(x => x.HotelName == hotel).Id;

            }
            else
            {
                ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName");
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name");
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "RoomTypeName");
            return View(reservation);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HotelId,RoomTypeId,ContactId,EntryDate,ReleaseDate,Status,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.CreateDate = DateTime.Now;
                reservation.CreatedBy = "Unknow";
                reservation.UpdateDate = DateTime.Now;
                reservation.UpdatedBy = "Unknow";
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name", reservation.ContactId);
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", reservation.HotelId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "RoomTypeName", reservation.RoomTypeId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name", reservation.ContactId);
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", reservation.HotelId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "RoomTypeName", reservation.RoomTypeId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoomId,ContactId,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name", reservation.ContactId);
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", reservation.HotelId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "RoomTypeName", reservation.RoomTypeId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
