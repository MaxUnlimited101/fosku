using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fosku_server.Models
{
    public class PaymentMethod : IFoskuModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public Customer Customer { get; set; }
        [MaxLength(20)]
        public string CardNumber { get; set; }
        /// <summary>
        /// 0 - cash;<br />
        /// 1 - bank card; <br />
        /// otherwise something else
        /// </summary>
        [Required]
        public int CardType { get; set; }
        public DateOnly ExpiryDate { get; set; }
        [MaxLength(200)]
        public string BillingAddress { get; set; }
    }
}
