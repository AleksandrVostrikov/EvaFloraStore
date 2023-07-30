using Calabonga.AspNetCore.AppDefinitions;
using EvaFloraStore.Models.SeedData;

namespace EvaFloraStore.Definitions
{
    public class SeedDataDefinition : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app)
        {
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
