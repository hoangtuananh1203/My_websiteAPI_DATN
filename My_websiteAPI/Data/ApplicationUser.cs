using Microsoft.AspNetCore.Identity;

namespace My_websiteAPI.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string? FirstName { get; set; } 
        public string?  LastName { get; set; } 
     
      
    }
}
