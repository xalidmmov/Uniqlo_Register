using Microsoft.AspNetCore.Identity;

namespace Uniqlo_main.Models
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
