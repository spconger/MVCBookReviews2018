using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBookReviews2018.Models
{
    public class NewBookClass
    {
        /// <summary>
        /// this is to help with the entry of a new book
        /// we are going to pretend for this example
        /// that each book just has one author. Adding
        /// multiple authors is more complicated
        /// and could cause confusion with the Donation
        /// excercise which is really quite simple
        /// </summary>
        public string Title { set; get; }
        public string ISBN { set; get; }

        public string Author { set; get; }
    }
}