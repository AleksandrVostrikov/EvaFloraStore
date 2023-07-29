using Calabonga.AspNetCore.AppDefinitions;
using EvaFloraStore.Infrastructure;
using EvaFloraStore.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Net;

namespace EvaFloraStore.Definitions
{
    public class ConfigurationDefinition : AppDefinition
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.Configure<CardDetails>(builder.Configuration.GetSection("CardDetails"));
            builder.Services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new MyViewLocationExpander());
            });
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("45.67.35.114"));
            });
        }
    }
}
