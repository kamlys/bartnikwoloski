using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bartnikwolski.ViewModels.Admin
{
    public class CreateItemViewModel
    {
        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Zdjęcie")]
        public HttpPostedFileBase PictureSource { get; set; }

        [Required]
        [Display(Name = "Strona")]
        public string SelectedPageTitle { get; set; }

        public IEnumerable<SelectListItem> Pages { get; set; }
    }
}