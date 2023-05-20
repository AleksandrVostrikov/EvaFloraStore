using EvaFloraStore.Models;

namespace EvaFloraStore.Repositories.Db
{
    public interface IOrderRepository
    {
        Task<IQueryable<Order>> GetOrders();
        Task SaveOrder(Order order);
        Task DeleteOrder(Guid orderId);
        Task UpdateOrderStatus(Guid orderId);
        Task UpdateArchiveStatus(Guid orderId);

    }
}
