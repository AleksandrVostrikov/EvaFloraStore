﻿using AutoMapper;
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
            return RedirectToAction("CreateProduct"); ;
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
                if (!(await _evaStoreRepository.GetCategoriesAsync()).Any(c => c.Name == model.Category.Name))
                {
                    await _evaStoreRepository.CreateCategoryAsync(model.Category);
                    TempData["SuccessMessage"] = "Категория успешно создана";
                }
                else
                {
                    TempData["ErrorMessage"] = "Такая категория уже есть";
                }
            }
            return RedirectToAction("CreateCategory");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _evaStoreRepository.GetCategoryAsync(id);
            await _evaStoreRepository.DeleteCategoryAsync(category);
            TempData["SuccessMessage"] = "Категория успешно удалена";
            return RedirectToAction("CreateCategory");
        }
    }
}
