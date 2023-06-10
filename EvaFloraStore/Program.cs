using EvaFloraStore.Data;
using EvaFloraStore.Infrastructure;
using EvaFloraStore.Models;
using EvaFloraStore.Models.SeedData;
using EvaFloraStore.Repositories.Db;
using EvaFloraStore.Repositories.EmailHandler;
using EvaFloraStore.Repositories.Image;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ItemsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EvaFloraConnectionString")));

builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnectionString")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddScoped<IEvaStoreRepository, EvaStoreRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IImageController, ImageController>();


builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(SessionCart.GetCart);

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<CardDetails>(builder.Configuration.GetSection("CardDetails"));
builder.Services.AddTransient<EmailService>();

builder.Services.AddTransient<IEmailHandler, EmailHandler>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationExpanders.Add(new MyViewLocationExpander());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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

SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);

app.Run();
