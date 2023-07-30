using Calabonga.AspNetCore.AppDefinitions;
using EvaFloraStore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EvaFloraStore.Definitions
{
    public class DbContextDefinition : AppDefinition
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ItemsDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("EvaFloraConnectionString")));
            builder.Services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnectionString")));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>();
        }

        public override void ConfigureApplication(WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        } 
    }
}
