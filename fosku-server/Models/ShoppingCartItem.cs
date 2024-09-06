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
        public uint Id { get; set; }
        [Required]
        public uint CartId { get; set; }
        [Required]
        public uint ProductId { get; set; }
        [Required]
        public uint Quantity { get; set; }
    }
}
