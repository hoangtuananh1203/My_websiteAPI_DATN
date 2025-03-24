using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.Model
{
    public class SignInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Password { get; set; } = null!;

    }
}
