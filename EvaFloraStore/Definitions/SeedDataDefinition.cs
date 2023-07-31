using Calabonga.AspNetCore.AppDefinitions;
using EvaFloraStore.Models.SeedData;

namespace EvaFloraStore.Definitions
{
    public class SeedDataDefinition : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                SeedData.EnsurePopulated(app);
                IdentitySeedData.EnsurePopulated(app);
            }
        }
    }
}
