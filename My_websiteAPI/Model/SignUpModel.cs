using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.Model
{
    public class SignUpModel
    {
    
        public string FirstName { get; set; } 

        public string LastName { get; set; } 
      
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = null!;
        [StringLength(50)]
        public string Password { get; set; } = null!;
        [StringLength(50)]
        public string ConfirmPassword { get; set; } = null!;

    }
}
