using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductController(
            IEvaStoreRepository evaStoreRepository,
            IMapper mapper)
        {
            _evaStoreRepository = evaStoreRepository;
            _mapper = mapper;
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
                var product = _mapper.Map<Product>(model.Product);
                await _evaStoreRepository.CreateProductAsync(product);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> CreateCategory(string returnUrl)
        {
            if (returnUrl == null)
            {
                returnUrl = TempData["ReturnUrl"]?.ToString();
            }
            TempData["returnUrl"] = returnUrl;
            return View(new CategoryAdding
            {
                Category = new(),
                Categories = (await _evaStoreRepository.GetCategoriesAsync()).ToList(),
                ReturnUrl= returnUrl
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryAdding model)
        {
            if (ModelState.IsValid)
            {
                await _evaStoreRepository.CreateCategoryAsync(model.Category);
            }
            return RedirectToAction("CreateCategory");
        }
    }
}
