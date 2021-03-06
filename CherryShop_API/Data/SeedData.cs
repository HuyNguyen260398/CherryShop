using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CherryShop_API.Data
{
    public static class SeedData
    {
        public async static Task Seed(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private async static Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Staff"))
            {
                var role = new IdentityRole
                {
                    Name = "Staff"
                };
                await roleManager.CreateAsync(role);
            }
        }

        private async static Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@test.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };
                var result = await userManager.CreateAsync(user, "P@ssw0rd");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }
            if (await userManager.FindByEmailAsync("staff@test.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "staff",
                    Email = "staff@test.com"
                };
                var result = await userManager.CreateAsync(user, "P@ssw0rd");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Staff");
                }
            }
        }
    }
}
