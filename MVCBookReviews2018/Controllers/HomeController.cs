using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBookReviews2018.Models; //add ref to Models

namespace MVCBookReviews2018.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // add ref to Entities
            BookReviewDbEntities db = new BookReviewDbEntities();
            //return books as a list
            return View(db.Books.ToList());
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