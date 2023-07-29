using Calabonga.AspNetCore.AppDefinitions;

namespace EvaFloraStore.Definitions
{
    public class MapperDefinition : AppDefinition
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program));
        } 
    }
}
