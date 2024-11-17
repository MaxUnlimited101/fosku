using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fosku_server.Models
{
    public class Product : IFoskuModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(1000)]
        public string Description { get; set; } = null!;
        public float Price { get; set; }
        public int StockQuantity { get; set; }
        public List<ProductImage> ProductImages { get; set; } = null!;
    }
}
