using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosku.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
