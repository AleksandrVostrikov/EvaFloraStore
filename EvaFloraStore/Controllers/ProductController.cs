using EvaFloraStore.Models;
using EvaFloraStore.Repositories.Db;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace EvaFloraStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IEvaStoreRepository _evaStoreRepository;

        public ProductController(IEvaStoreRepository evaStoreRepository)
        {
            _evaStoreRepository = evaStoreRepository;
        }

        public IActionResult CreateProduct() => View(new Product());
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _evaStoreRepository.CreateProductAsync(product);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
