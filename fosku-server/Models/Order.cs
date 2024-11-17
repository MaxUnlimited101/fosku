using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fosku_server.Helpers;

namespace fosku_server.Models
{
    public class Order : IFoskuModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int TotalAmount { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string ShippingAddress { get; set; } = null!;

        [Required]
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
