using Calabonga.AspNetCore.AppDefinitions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EvaFloraStore.Definitions
{
    public class BasicDefinition : AppDefinition
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddSession();
            
            builder.Services.AddDistributedMemoryCache();

        }
        public override void ConfigureApplication(WebApplication app)
        {
            
        } 
    }
}
