using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        //var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //string[] roles = { "Admin", "Manager", "User" };

        //foreach (var role in roles)
        //{
        //    if (!await roleManager.RoleExistsAsync(role))
        //    {
        //        await roleManager.CreateAsync(new IdentityRole(role));
        //    }
        //}
    }
}
