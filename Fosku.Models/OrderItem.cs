using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Fosku.Models
{
    public class OrderItem
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public uint OrderId { get; set; }
        [Required]
        public uint ProductId { get; set; }
        [Required]
        public uint Quantity { get; set; }
        [Required]
        public uint UnitPrice { get; set; }
    }
}
