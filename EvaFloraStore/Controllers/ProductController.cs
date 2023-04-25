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

        public async Task<IActionResult> EditProduct(Guid id)
        {
            if (id != Guid.Empty)
            {
                var product = await _evaStoreRepository.GetProductAsync(id);
                var editingProduct = _mapper.Map<ProductAdding>(product);
                editingProduct.Categories = (await _evaStoreRepository.GetCategoriesAsync()).ToList();
                return View(editingProduct);
            }
            else
            {
                return View(new ProductAdding
                {
                    Product = new(),
                    Categories = (await _evaStoreRepository.GetCategoriesAsync()).ToList()
                });
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductAdding model)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(model.Product);
                if (product.Id != Guid.Empty)
                {
                    await _evaStoreRepository.SaveAsync(product);
                }
                else
                {
                    await _evaStoreRepository.CreateProductAsync(product);
                }
            }
            return RedirectToAction("GetProductList");
        }
        

        public async Task<IActionResult> GetProductList(string returnUrl)
        {
            if (returnUrl == null)
            {
                returnUrl = TempData["ProductReturnUrl"]?.ToString();
            }
            TempData["ProductReturnUrl"] = returnUrl;
            return View(new AdminProductListViewModel
            {
                ReturnUrl = returnUrl,
                Products = (await _evaStoreRepository.GetProductsAsync())
                .OrderBy(p => p.Name)
                .ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _evaStoreRepository.GetProductAsync(id);
            await _evaStoreRepository.DeleteProductAsync(product);
            TempData["SuccessMessage"] = "Продукт успено удален";
            return RedirectToAction("GetProductList");
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
