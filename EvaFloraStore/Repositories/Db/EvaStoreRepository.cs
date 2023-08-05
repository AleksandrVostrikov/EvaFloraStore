using EvaFloraStore.Data;
using EvaFloraStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace EvaFloraStore.Repositories.Db
{
    public class EvaStoreRepository : IEvaStoreRepository
    {
        private readonly ItemsDbContext _dbContext;

        public EvaStoreRepository(ItemsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IQueryable<Product>> GetProductsAsync()
        {
            return await Task.FromResult(_dbContext.Products);
        }
        public async Task<IQueryable<Category>> GetCategoriesAsync()
        {
            return await Task.FromResult(_dbContext.Categories);
        }
        public async Task CreateCategoryAsync(Category c)
        {
            await _dbContext.Categories.AddAsync(c);
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateProductAsync(Product p)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == p.Category.Name);
            if (category == null)
            {
                await _dbContext.Products.AddAsync(p);
            }
            else
            {
                p.Category = category;
                await _dbContext.Products.AddAsync(p);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteCategoryAsync(Category c)
        {
            _dbContext.Categories.Remove(c);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product p)
        {
            _dbContext.Products.Remove(p);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveAsync(Product p)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == p.Category.Name);
            p.Category = category;
            _dbContext.Products.Update(p);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            return await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? new Product() { Name = "Нет такого продукта" };
        }

        public async Task<Category> GetCategoryAsync(Guid id)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> GetCountProductsAsync()
        {
            return await _dbContext.Products.CountAsync();
        } 
        public async Task<int> GetCountProductsAsync(string category)
        {
            return await _dbContext.Products
                .Where(p => p.Category.Name == category)
                .CountAsync();
        } 
    }
}
