using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBookReviews2018.Models;

namespace MVCBookReviews2018.Controllers
{
    public class BookReviewController : Controller
    {
        //declare the book reviews entities
        BookReviewDbEntities db = new BookReviewDbEntities();
        // GET: BookReview
        public ActionResult Index()
        {
            //check to make sure they are logged in
            if(Session["userKey"] == null)
            {
                Message m = new Message();
                m.MessageText = "You must be logged in to add a review";
                return RedirectToAction("Result", m);
            }
            //we are going to use the view bag object to store a list
            //of available books
            ViewBag.BookKey = new SelectList(db.Books, "BookKey", "BookTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index([Bind(Include ="BookKey,ReviewerKey, ReviewDate,ReviewTitle,ReviewRating,ReviewText")]Review r)
        {
            //we basically just take in the review Class
            //I am assigning it the current date
            //We will get the userKey from the session variable
            r.ReviewDate = DateTime.Now;
            r.ReviewerKey = (int)Session["userKey"];
            db.Reviews.Add(r);
            db.SaveChanges();

            Message m = new Message();
            m.MessageText = "Thanks for the Review";
            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}