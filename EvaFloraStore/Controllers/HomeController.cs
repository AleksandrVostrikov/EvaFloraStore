using EvaFloraStore.Models;
using EvaFloraStore.Models.ViewModels;
using EvaFloraStore.Repositories.Db;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EvaFloraStore.Controllers
{
    public class HomeController : Controller
    {
        public int PageSize = 6;

        private readonly IEvaStoreRepository _evaStoreRepository;

        public HomeController( 
            IEvaStoreRepository evaStoreRepository)
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

            int totalItems = category == null ?
                    await _evaStoreRepository.GetCountProductsAsync() : 
                    await _evaStoreRepository.GetCountProductsAsync(category);
            
            var productListViewModel = new ProductListViewModel
            {
                Products = products,
                ProductsCount = totalItems,
                CurrentCategory = category
            };
            productListViewModel.PageDivider = new()
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = totalItems
            };

            return View(productListViewModel);
        }

        public async Task<IActionResult> ProductDetails(Guid id, string returnUrl)
        {
            ProductDetailsViewModel viewModel = new()
            {
                Product = await _evaStoreRepository.GetProductAsync(id),
                ReturnUrl = returnUrl ?? "/"
            };
            return View(viewModel);
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