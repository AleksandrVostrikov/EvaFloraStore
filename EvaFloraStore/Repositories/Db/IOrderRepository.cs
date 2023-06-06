using EvaFloraStore.Models;

namespace EvaFloraStore.Repositories.Db
{
    public interface IOrderRepository
    {
        Task<IQueryable<Order>> GetOrders();
        Task<Order> GetOrder(Guid orderId);
        Task SaveOrder(Order order);
        Task DeleteOrder(Guid orderId);
        Task UpdateOrder(Order order);
        Task UpdateOrderStatus(Guid orderId);
        Task UpdateArchiveStatus(Guid orderId);

    }
}
