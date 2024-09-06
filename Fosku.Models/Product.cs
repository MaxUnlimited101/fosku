using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosku.Models
{
    public class Product
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public uint StockQuantity { get; set; }
        public uint CategoryId { get; set; }
        [Required]
        public uint ProductImageId { get; set; }
    }
}
