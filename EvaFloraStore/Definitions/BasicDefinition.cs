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

            app.MapControllerRoute("admin",
                "/AleksandrVostrikov",
                new { Controller = "Account", action = "Login" });

            app.MapControllerRoute("catpage",
                "{category}/Page{productPage:int}",
                new { Controller = "Home", action = "Index" });

            app.MapControllerRoute("page", "Page{productPage:int}",
                new { Controller = "Home", action = "Index" });

            app.MapControllerRoute("category", "{category}",
                new { Controller = "Home", action = "Index" });

            app.MapControllerRoute("pagedividing",
                "Products/Page{productPage}",
                new { Controller = "Home", action = "Index" });

            app.MapRazorPages();
            app.MapDefaultControllerRoute();

        }

    }
}
