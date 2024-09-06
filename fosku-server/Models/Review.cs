using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosku.Models
{
    public class Review
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public uint UserId { get; set; }
        [Required]
        public uint ProductId { get; set; }
        [Required]
        [Range(1, 5, MaximumIsExclusive = false, MinimumIsExclusive = false)]
        public uint Rating { get; set; }
        public string Comment { get; set; }
        [Required]
        public DateOnly CreatedAt { get; set; }
    }
}
