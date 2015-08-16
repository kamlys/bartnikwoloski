using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bartnikwolski.ViewModels.Admin
{
    public class EditViewModel
    {
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Treść")]
        public string Content { get; set; }

        [Display(Name = "Zdjęcie")]
        public string PictureSource { get; set; }

        [Display(Name="Zmień")]
        public HttpPostedFileBase Picture { get; set; }

        public bool HasItems { get; set; }
    }
}