using Calabonga.AspNetCore.AppDefinitions;

namespace EvaFloraStore.Definitions
{
    public class MapDefinition : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app)
        {
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
