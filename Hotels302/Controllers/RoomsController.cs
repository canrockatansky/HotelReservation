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
    public class RoomsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.Hotel).Include(r => r.RoomType);
            return View(rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            var room = new Room();
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName");
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "RoomTypeName");
            return View(room);
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HotelId,RoomNo,RoomTypeId,Cost,Status,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] Room room, IEnumerable<HttpPostedFileBase> ResimDosya)
        {
            
            if (ModelState.IsValid)
            {
                if (ResimDosya != null)
                {
                    foreach (var item in ResimDosya)
                    {
                        item.SaveAs(Server.MapPath($"/Content/images/{item.FileName}"));
                        room.Foto = "/Content/images/" + item.FileName;
                    }
                }

                room.CreateDate = DateTime.Now;
                room.CreatedBy = "Unknow";
                room.UpdateDate = DateTime.Now;
                room.UpdatedBy = "Unknow";
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", room.HotelId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "RoomTypeName", room.RoomTypeId);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", room.HotelId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "RoomTypeName", room.RoomTypeId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HotelId,RoomNo,RoomTypeId,Cost,Status,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] Room room)
        {
            if (ModelState.IsValid)
            {
               room.UpdateDate = DateTime.Now;
               room.UpdatedBy = "Unknow";
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", room.HotelId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "RoomTypeName", room.RoomTypeId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
