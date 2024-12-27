using Microsoft.AspNetCore.Identity;
using Uniqlo_main.Enums;
using Uniqlo_main.Models;

namespace Uniqlo_main.Extensions
{
    public static class SeedExtension
    {
        public static void UseUserSeed(this IApplicationBuilder app)
        {
            using (var scope=app.ApplicationServices.CreateScope())
            {
                var userMnager=scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!roleManager.Roles.Any())
                {
                    foreach (Roles item in Enum.GetValues(typeof(Roles)))
                    {
                        roleManager.CreateAsync(new IdentityRole(item.ToString())).Wait();
                    }
                }
                if (!userMnager.Users.Any(x => x.NormalizedUserName == "ADMIN"))
                {
                    User u= new User
                    {
                        FullName = "admin",
                        UserName = "admin",
                        Email = "admin@gmail.com",
                        ProfileImageUrl = "photo.jpg"
                    };
                    userMnager.CreateAsync(u,"123").Wait();
                    userMnager.AddToRoleAsync(u, nameof(Roles.Admin)).Wait();
                }
                
            }

        }
    }
}
