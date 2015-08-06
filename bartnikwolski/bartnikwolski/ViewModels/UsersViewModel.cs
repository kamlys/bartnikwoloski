using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bartnikwolski.ViewModels
{
    public class UsersViewModel
    {
        public class LoginViewModel
        {
            [Required]
            public string Login { get; set; }

            [Required]
            [Display(Name = "Hasło")]
            public string Password { get; set; }
        }
    }
}