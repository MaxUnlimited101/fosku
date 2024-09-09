using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosku.Models
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
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
