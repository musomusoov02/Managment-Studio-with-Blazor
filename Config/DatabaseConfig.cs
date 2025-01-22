using System.ComponentModel.DataAnnotations;

namespace Managment_Studio_with_Blazor.Config
{
    public class DatabaseConfig
    {
        [Required(ErrorMessage = "Server maydoni kerak")]
        public string Server { get; set; }

        [Required(ErrorMessage = "Database maydoni kerak")]
        public string Database { get; set; }

        [Required(ErrorMessage = "Port maydoni kerak")]
        [Range(1024, 65535, ErrorMessage = "Port faqat raqam bo'lishi kerak va 1024 va 65535 orasida bo'lishi kerak.")]
        public int Port { get; set; }

        [Required(ErrorMessage = "Username maydoni kerak")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password maydoni kerak")]
        public string Password { get; set; }
    }
}
