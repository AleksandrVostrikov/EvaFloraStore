using Calabonga.AspNetCore.AppDefinitions;
using EvaFloraStore.Models;
using EvaFloraStore.Repositories.Db;
using EvaFloraStore.Repositories.Image;

namespace EvaFloraStore.Definitions
{
    public class DependencyContainerDefinition : AppDefinition
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEvaStoreRepository, EvaStoreRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IImageController, ImageController>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(SessionCart.GetCart);
        }
    }
}
