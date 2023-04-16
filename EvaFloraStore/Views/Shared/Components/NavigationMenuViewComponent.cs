using EvaFloraStore.Repositories.Db;
using Microsoft.AspNetCore.Mvc;

namespace EvaFloraStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IEvaStoreRepository _evaStoreRepository;

        public NavigationMenuViewComponent(IEvaStoreRepository evaStoreRepository)
        {
            _evaStoreRepository = evaStoreRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View((await _evaStoreRepository.GetCategoriesAsync())
                .Select(c => c.Name)
                .Distinct()
                .OrderBy(c => c));
        }
    }
}
