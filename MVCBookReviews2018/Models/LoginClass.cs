using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBookReviews2018.Models
{
    public class LoginClass
    {
        /// <summary>
        /// this class is just to pass the login
        ///information from the form to the controller
        ///where it will be passed to the stored procedure
        ///for processing
        /// </summary>
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}