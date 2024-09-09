﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosku.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        [Range(1, 5, MaximumIsExclusive = false, MinimumIsExclusive = false)]
        public int Rating { get; set; }
        [MaxLength(400)]
        public string Comment { get; set; }
        [Required]
        public DateOnly CreatedAt { get; set; }
    }
}
