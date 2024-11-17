using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace fosku_server.Models
{
    public class OrderItem : IFoskuModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; } = null!;

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int UnitPrice { get; set; }
    }
}
