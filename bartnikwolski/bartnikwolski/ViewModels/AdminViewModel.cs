using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bartnikwolski.Models;
using System.ComponentModel.DataAnnotations;

namespace bartnikwolski.ViewModels
{
    public class AdminViewModel
    {
        public class Index
        {
            public IList<Page> PageList { get; set; }
        }

        public class Edit
        {
            public int PageId { get; set; }

            public string Title { get; set; }

            public string Content { get; set; }

            [Display(Name = "Zdjęcie")]
            public HttpPostedFileBase Picture { get; set; }
        }
    }
}