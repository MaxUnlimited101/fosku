using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int UserId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        [MaxLength(200)]
        public string ShippingAddress { get; set; }
        /// <summary>
        /// TODO: think this out
        /// </summary>
        [Required]
        public int OrderStatus { get; set; }
        /// <summary>
        /// TODO: think this out as well
        /// </summary>
        [Required]
        public int PaymentStatus { get; set; }
    }
}
