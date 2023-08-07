using Calabonga.AspNetCore.AppDefinitions;
using Microsoft.AspNetCore.HttpOverrides;

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
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
        }

    }
}
