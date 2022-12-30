using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookPublishers.ViewModel
{
    public class EditorEditModel
    {
        public int EditorId { get; set; }
        [Required, Display(Name = "Editor Name"), StringLength(50)]
        public string EditorName { get; set; }
        [Required, Display(Name = "Mobile No"), StringLength(50)]
        public string MobileNo { get; set; }
        [Required]
        public int PublisherId { get; set; }
    }
}