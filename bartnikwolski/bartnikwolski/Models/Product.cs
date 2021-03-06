﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bartnikwolski.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PictureSource { get; set; }

        [Required]
        public bool IsProduct { get; set; }

        [Required]
        public bool IsSpecial { get; set; }
    }
}