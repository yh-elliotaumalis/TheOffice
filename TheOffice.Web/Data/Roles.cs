using Microsoft.AspNetCore.Identity;

namespace TheOffice.Web.Data
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Employee = "Employee";
        public const string Customer = "Customer";

        public static readonly string[] All = [Admin, Employee, Customer];

        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in All)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
