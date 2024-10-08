﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosku.Models
{
    public class ProductImage
    {
        [Key]
        public uint Id { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string AltText { get; set; }
    }
}
