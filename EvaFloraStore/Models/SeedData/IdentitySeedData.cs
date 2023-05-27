using EvaFloraStore.Data;
using EvaFloraStore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EvaFloraStore.Models.SeedData
{
    public static class IdentitySeedData
    {
        public static async void EnsurePopulated(IApplicationBuilder app)
        {

            IConfiguration configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
            string adminUser = configuration["UserSettings:Username"];
            string adminPassword = configuration["UserSettings:UserPassword"];

            IdentityDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<IdentityDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser(adminUser);
                user.Email = "Bray3636@example.com";
                user.PhoneNumber = "89265687845";
                await userManager.CreateAsync(user, adminPassword);
            }
        }

    }
}
