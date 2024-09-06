using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosku.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public uint TotalAmount { get; set; }
        [Required]
        public uint UserId { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        /// <summary>
        /// TODO: think this out
        /// </summary>
        [Required]
        public uint OrderStatus { get; set; }
        /// <summary>
        /// TODO: think this out as well
        /// </summary>
        [Required]
        public uint PaymentStatus { get; set; }
    }
}
