using EvaFloraStore.Models;

namespace EvaFloraStore.Repositories.Db
{
    public interface IEvaStoreRepository
    {
        Task<IQueryable<Product>> GetProductsAsync();
        Task<IQueryable<Category>> GetCategoriesAsync();
        Task<Product> GetProductAsync(Guid id);
        Task DeleteProductAsync(Product p);
        Task CreateCategoryAsync(Category c);
        Task DeleteCategoryAsync(Category c);
        Task SaveAsync();
    }
}
