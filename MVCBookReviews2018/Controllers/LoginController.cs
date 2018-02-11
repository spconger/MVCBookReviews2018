using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBookReviews2018.Models; //add models

namespace MVCBookReviews2018.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //empty Index returns the blank form
        public ActionResult Index()
        {
            return View();
        }

        //this index processes the submitted form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include ="UserName, Password")]LoginClass lc)
        {
            //make a connection to the Entities
            BookReviewDbEntities db = new BookReviewDbEntities();
            //use the stored procedure prebuilt in the database
            int result = db.usp_ReviewerLogin(lc.UserName, lc.Password);
            //if the result is valid
            if(result != -1)
            {
                //look up the users key
                var uID = (from r in db.Reviewers
                           where r.ReviewerUserName.Equals(lc.UserName)
                           select r.ReviewerKey).FirstOrDefault();

                //cast it to an int
                int key = (int)uID;

                //strore it in a session key
                Session["userKey"] = key;

                //create a message class and pass the string to the result view
                Message msg = new Message("Welcome, " + lc.UserName + ". You can now add and review books.");
                return RedirectToAction("Result", msg);
                           
            }
            //if it fails pass this message to the result view
            Message message = new Message("Invalid Login");
            return View("Result", message);
        }

        //you must have an ActionResult method for the Result
        //that takes the message as a parameter
        public ActionResult Result(Message msg)
        {
            return View(msg);
        }
    }
}