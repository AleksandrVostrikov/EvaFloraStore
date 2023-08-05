using EvaFloraStore.Models;

namespace EvaFloraStore.Repositories.Db
{
    public interface IEvaStoreRepository
    {
        Task<IQueryable<Product>> GetProductsAsync();
        Task<IQueryable<Category>> GetCategoriesAsync();
        Task<int> GetCountProductsAsync();
        Task<int> GetCountProductsAsync(string c);
        Task<Product> GetProductAsync(Guid id);
        Task<Category> GetCategoryAsync(Guid id);
        Task CreateProductAsync(Product p);
        Task DeleteProductAsync(Product p);
        Task CreateCategoryAsync(Category c);
        Task DeleteCategoryAsync(Category c);
        Task SaveAsync(Product p);
    }
}
