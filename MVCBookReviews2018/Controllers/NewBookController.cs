using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBookReviews2018.Models;

namespace MVCBookReviews2018.Controllers
{
    public class NewBookController : Controller
    {
        /// <summary>
        /// As noted in the comments of the NewBookClass
        /// This is going to be a simplified version
        /// that assumes a single author. I am doing this
        /// to make it more like the donation excercise
        /// of the assignment.
        /// </summary>
        /// <returns></returns>
        // GET: NewBook
        // I am going to let the index be the entry page
        //with the form for the new book
        public ActionResult Index()
        {
            //we check for the session key to see if they are logged in
            //for the donate assignment you can also get their 
            //person key from the session for the donation
            //  int pKey=(int)Session["personKey]
            //using whatever you named your session in login
            if (Session["userKey"] == null)
            {
                Message m = new Message("You must be logged in to enter new books");
                return RedirectToAction("Result", m);
            }
            return View();
        }

        /* ********************************************************
         * commenting out the simpler version to replace it with 
        the classroom version

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include ="Title, ISBN, Author")]NewBookClass nb)
        {
            BookReviewDbEntities db = new BookReviewDbEntities();
            //create a new instance of Book and map the fields from our class to it
            Book b = new Book();
            b.BookTitle = nb.Title;
            b.BookISBN = nb.ISBN;
            b.BookEntryDate = DateTime.Now;
            //add this book to the collection of books
            db.Books.Add(b);

            //do the same with author
            Author a = new Author();
            a.AuthorName = nb.Author;
            db.Authors.Add(a);

            //save the new book and author to the database
            db.SaveChanges();

            Message m = new Message("Book added");
            return RedirectToAction("Result", m);
        }
        *************************************************/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Title, ISBN, Author")]NewBookClass nb)
        {
            BookReviewDbEntities db = new BookReviewDbEntities();
            Author a = new Author();
            a.AuthorName = nb.Author;
            db.Authors.Add(a);
            db.SaveChanges();
            // for donation get userkey from the session
            Book b = new Book();
            b.BookTitle = nb.Title;
            b.BookISBN = nb.ISBN;
            b.BookEntryDate = DateTime.Now;
            Author author = db.Authors.FirstOrDefault
                (x => x.AuthorName == nb.Author);
            b.Authors.Add(author);

            db.Books.Add(b);
            db.SaveChanges();

            Message m = new Message();
            m.MessageText = "Thank you, the book has been added";

            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}