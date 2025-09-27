using Microsoft.AspNetCore.Identity;

namespace deneme_2.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
