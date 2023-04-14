using EvaFloraStore.Models;
using EvaFloraStore.Models.ViewModels;
using EvaFloraStore.Repositories.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;

namespace EvaFloraStore.Controllers
{
    public class HomeController : Controller
    {
        public int PageSize = 4;

        private readonly IEvaStoreRepository _evaStoreRepository;

        public HomeController( IEvaStoreRepository evaStoreRepository)
        {
            _evaStoreRepository = evaStoreRepository;
        }

        public async Task<IActionResult> Index(string category,  int productPage=1)
        {
            var allProducts = (await _evaStoreRepository.GetProductsAsync())
                .Where(p => p.IsVisible == true);
            var products = allProducts
                .Where(p => p.Category.Name == category || category == null)
                .OrderBy(p => p.Name)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize).ToList();

            var productViewModels = products.Select((p, i) => new ProductViewModel
            {
                DispalyedProduct = p,
                PositionInList = i
            });
            int totalItems = category == null ?
                    allProducts.Count() : allProducts
                    .Where(p => p.Category.Name == category).Count();
            var productListViewModel = new ProductListViewModel
            {
                Products = productViewModels,
                ProductsCount = totalItems
            };
            productListViewModel.PageDivider = new()
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = totalItems
            };

            return View(productListViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}