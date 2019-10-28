using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotels302.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
       
        Models.AppDbContext db = new Models.AppDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var hotels = db.Hotels.ToList();
            return View(hotels);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}