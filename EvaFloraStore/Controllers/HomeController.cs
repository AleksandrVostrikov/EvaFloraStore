using EvaFloraStore.Models;
using EvaFloraStore.Models.ViewModels;
using EvaFloraStore.Repositories.Db;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace EvaFloraStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEvaStoreRepository _evaStoreRepository;

        public HomeController( IEvaStoreRepository evaStoreRepository)
        {
            _evaStoreRepository = evaStoreRepository;
        }

        public  async Task<IActionResult> Index()
        {
            var items = (await _evaStoreRepository.GetProductsAsync()).OrderBy(p => p.Name).ToList();
            ProductListViewModel productListViewModel = new();
            productListViewModel.Products = items.Select((item, index) => new ProductViewModel
            {
                DispalyedProduct = item,
                PositionInList = index
            }).ToList();
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