using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosku.Models
{
    public class Category
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public uint ParentCategoryId { get; set; }
    }
}
