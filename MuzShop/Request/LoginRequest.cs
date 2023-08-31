using System.ComponentModel.DataAnnotations;

namespace MuzShop.Request
{
    public class LoginRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
