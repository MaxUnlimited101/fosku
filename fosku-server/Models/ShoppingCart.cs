using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosku.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; } = null!;
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
