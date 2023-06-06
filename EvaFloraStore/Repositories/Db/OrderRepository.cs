using EvaFloraStore.Data;
using EvaFloraStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaFloraStore.Repositories.Db
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ItemsDbContext _dbContext;

        public OrderRepository(ItemsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Order>> GetOrders()
        {
            return await Task.FromResult(_dbContext.Orders
                .Include(o => o.Lines)
                .ThenInclude(l => l.Product)); 
        }

        public async Task SaveOrder(Order order)
        {
            _dbContext.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderId == Guid.Empty)
            {
                await _dbContext.Orders.AddAsync(order);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrder(Guid orderId)
        {
            var order = await _dbContext.Orders.Include(o => o.Lines).FirstOrDefaultAsync(o => o.OrderId == orderId);
            _dbContext.RemoveRange(order.Lines);
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateArchiveStatus(Guid orderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order.Archive)
            {
                order.Archive = false;
            }
            else
            {
                order.Archive = true;
            }
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderStatus(Guid orderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order.Shipped)
            {
                order.Shipped = false;
            }
            else
            {
                order.Shipped = true;
            }
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
    }
}
