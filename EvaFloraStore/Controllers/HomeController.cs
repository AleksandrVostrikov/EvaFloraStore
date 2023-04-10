using EvaFloraStore.Models;
using EvaFloraStore.Models.ViewModels;
using EvaFloraStore.Repositories.Db;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            return View(new ProductListViewModel
            {
                Products = await _evaStoreRepository.GetProductsAsync()
            });
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