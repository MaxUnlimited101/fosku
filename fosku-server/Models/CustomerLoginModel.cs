using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace fosku_server.Models
{
    public class CustomerLoginModel
    {
        public string Email { get; set; }
        /// <summary>
        /// SHA256 hash
        /// </summary>
        public string PasswordHash { get; set; }
    }
}
