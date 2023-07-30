using Calabonga.AspNetCore.AppDefinitions;
using EvaFloraStore.Infrastructure;
using EvaFloraStore.Repositories.EmailHandler;

namespace EvaFloraStore.Definitions
{
    public class EmailSenderDefinition : AppDefinition
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<EmailService>();
            builder.Services.AddTransient<IEmailHandler, EmailHandler>();
        }
   
    }
}
