using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace fosku_server.Models
{
    public class Customer : Person
    {
        [MaxLength(100)]
        public string Address { get; set; } = null!;

        [MaxLength(50)]
        public string City { get; set; } = null!;

        [MaxLength(50)]
        public string Country { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
