﻿using EvaFloraStore.Models;

namespace EvaFloraStore.Repositories.Db
{
    public interface IEvaStoreRepository
    {
        Task<IQueryable<Product>> GetProductsAsync();
        Task<IQueryable<Category>> GetCategoriesAsync();
        Task CreateProductAsync(Product p);
        Task DeleteProductAsync(Product p);
        Task CreateCategoryAsync(Category c);
        Task DeleteCategoryAsync(Category c);
        Task SaveAsync();
    }
}
