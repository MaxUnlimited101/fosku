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
        public uint Id { get; set; }
        [Required]
        public uint UserId { get; set; }
        public string CardNumber { get; set; }
        /// <summary>
        /// 0 - cash;<br />
        /// 1 - bank card; <br />
        /// otherwise something else
        /// </summary>
        [Required]
        public uint CardType { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public string BillingAddress { get; set; }
    }
}
