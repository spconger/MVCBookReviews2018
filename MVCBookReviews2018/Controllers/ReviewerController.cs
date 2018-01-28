using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBookReviews2018.Models;

namespace MVCBookReviews2018.Controllers
{
    public class ReviewerController : Controller
    {
        //connect with the data entities
        BookReviewDbEntities db = new BookReviewDbEntities();
        // GET: Reviewer
        //I am going to add a list of reviews to Index, This is not
        //required in the the assignment. In the assignment you can
        //do the registration in the Index if you prefer.
        public ActionResult Index()
        {
            return View(db.Reviewers.ToList());
        }

        //you need an empty method for Register to display the empty form
        public ActionResult Register()
        {
            return View();
        }

        //the post and antiforgery version of the method
        //takes the content of the form and passes it to the method
        //in the parametes we list all the fields we need of the class
        //and it Binds them to an instance of the class
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include ="ReviewerUserName, ReviewerFirstName, ReviewerLastName, ReviewerEmail, ReviewPlainPassword")]Reviewer reviewer)
        {
            //we are passing the fields from the class to the stored procedure
            //which will write the reviewer and create the hashed password.
            int result = db.usp_NewReviewer(reviewer.ReviewerUserName, reviewer.ReviewerFirstName, reviewer.ReviewerLastName, reviewer.ReviewerEmail, reviewer.ReviewPlainPassword);
            if(result != -1)
            {
                return RedirectToAction("Index");
            }
            return View(reviewer);
        }
    }
}