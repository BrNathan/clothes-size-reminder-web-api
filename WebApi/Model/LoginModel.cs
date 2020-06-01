using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
