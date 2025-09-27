using deneme_2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace deneme_2.Seed
{
    public  class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var logger = serviceProvider.GetRequiredService<ILogger<IdentitySeeder>>();
            // 1. Rolları yarat
            string[] roles = { "SuperAdmin", "Admin", "Member" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // 2. Admin istifadəçi yarat
            if (await userManager.FindByNameAsync("SuperAdmin") == null)
            {
                var admin = new AppUser
                {
                    FullName = "SuperAdmin",
                    UserName = "SuperAdmin",
                    Email = "Mehemmedmemmedov@gmail.com"
                };

                var result = await userManager.CreateAsync(admin, "Admin123*");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                    logger.LogInformation("Admin user created successfully.");
                }
                else
                {
                    logger.LogError("Error creating admin user: {Errors}",
                                    string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
