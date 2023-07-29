using Calabonga.AspNetCore.AppDefinitions;
using EvaFloraStore.Data;
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
        } 
    }
}
