using EvaFloraStore.Models;
using EvaFloraStore.Models.ViewModels;
using EvaFloraStore.Repositories.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> CreateProduct()
        {
            
            return View(new ProductAdding
            {
                Product = new(),
                Categories = (await _evaStoreRepository.GetCategoriesAsync()).ToList()
            });
        }  
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductAdding model)
        {
            if (ModelState.IsValid)
            {
                Product product = new()
                {
                    Name = model.Product.Name,
                    Description = model.Product.Description,
                    ShortDescription = model.Product.ShortDescription,
                    Price = model.Product.Price,
                    CategoryId = model.Product.CategoryId,
                    ImageUrl = model.Product.ImageUrl,
                    IsVisible = model.Product.IsVisible,
                    Category = model.Product.Category
                };
                await _evaStoreRepository.CreateProductAsync(product);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
