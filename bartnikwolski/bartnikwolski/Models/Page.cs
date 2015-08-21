using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bartnikwolski.Models
{
    public class Page
    {
        [Required]
        [Key]
        public string Title { get; set; }
        public string Content { get; set; }
        public string PictureSource { get; set; }
        public bool HasProducts { get; set; }
    }
}