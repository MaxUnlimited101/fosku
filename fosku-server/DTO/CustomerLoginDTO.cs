using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace fosku_server.DTO
{
    //DTO
    public class CustomerLoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// SHA256 hash
        /// </summary>
        public string PasswordHash { get; set; }
    }
}
