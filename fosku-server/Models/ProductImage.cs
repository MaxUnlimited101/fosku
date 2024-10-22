using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fosku_server.Models
{
    /// <summary>
    /// This is used for the relation "1 product - many images"
    /// </summary>
    public class ProductImage : IFoskuModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(100)]
        public string AltText { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
