using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bartnikwolski.Models
{
    public class Page
    {
        public int PageId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public string PictureSource { get; set; }
    }
}