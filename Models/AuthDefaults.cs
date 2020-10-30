using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNET.Models
{
    public class AuthDefaults
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                InvoiceDbContext dbContext = scope.ServiceProvider
                    .GetService<InvoiceDbContext>();

                dbContext.Database.Migrate();

                UserManager<IdentityUser> userManager = scope.ServiceProvider
                    .GetService<UserManager<IdentityUser>>();

                string[] roles = new string[] { "admin", "user" };

                foreach (var role in roles)
                {
                    RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(dbContext);

                    if (!dbContext.Roles.Any(r => r.Name == role))
                    {
                        IdentityRole newRole = new IdentityRole
                        {
                            Name = role,
                            NormalizedName = userManager.NormalizeName(role)
                        };

                        await roleStore.CreateAsync(newRole);
                    }
                }

                IdentityUser user = new IdentityUser
                {
                    UserName = "admin",
                    NormalizedUserName = "ADMIN"
                };

                if (!dbContext.Users.Any(u => u.UserName == user.UserName))
                {
                    PasswordHasher<IdentityUser> pwHasher = new PasswordHasher<IdentityUser>();
                    user.PasswordHash = pwHasher.HashPassword(user, "Admin123!");

                    UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(dbContext);
                    await userStore.CreateAsync(user);
                }

                IdentityUser admin = await userManager.FindByNameAsync("admin");

                if (admin != null && !(await userManager.IsInRoleAsync(admin, "admin")))
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
