﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookPublishers.ViewModel
{
    public class AuthorEditModel
    {
        public int AuthorId { get; set; }
        [Required, StringLength(50), Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        [Required,  Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        
        public HttpPostedFileBase picture { get; set; }
        public int BookId { get; set; }
    }
}