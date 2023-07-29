using Calabonga.AspNetCore.AppDefinitions;
using EvaFloraStore.Data;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.AddDefinitions(typeof(Program));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


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

//SeedData.EnsurePopulated(app);
//IdentitySeedData.EnsurePopulated(app);

app.Run();
