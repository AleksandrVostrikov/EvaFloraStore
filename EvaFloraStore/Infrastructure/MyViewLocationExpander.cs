using Microsoft.AspNetCore.Mvc.Razor;

namespace EvaFloraStore.Infrastructure
{
    public class MyViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context) { }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var res = new[] 
            { 
                "/Views/Admin/{1}/{0}.cshtml",
                "/Views/{1}/{0}.cshtml",
                "/Views/Shared/{0}.cshtml",
            };
            return res;
        }

    }
}
