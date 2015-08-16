using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bartnikwolski.ViewModels.Admin
{
    public class AccountSettingsViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [Display(Name="Stare hasło")]
        public string OldPassword { get; set; }

        [Display(Name="Nowe hasło")]
        public string Password { get; set; }

        [Display(Name = "Powtórz hasło")]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
    }
}