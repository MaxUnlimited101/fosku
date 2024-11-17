using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fosku_server.Models
{
    public class Category : IFoskuModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(400)]
        public string Description { get; set; } = null!;

        public int ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; } = null!;
    }
}
