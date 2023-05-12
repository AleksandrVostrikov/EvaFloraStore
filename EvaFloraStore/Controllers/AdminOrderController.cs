using EvaFloraStore.Models.ViewModels;
using EvaFloraStore.Repositories.Db;
using Microsoft.AspNetCore.Mvc;

namespace EvaFloraStore.Controllers
{
    public class AdminOrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public AdminOrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> GetOrders()
        {
            var fullOrders = await _orderRepository.GetOrders();
            var unShippedOrders = fullOrders.Where(o => o.Shipped == false);
            var shippedOrders = fullOrders.Where(o => o.Shipped == true);
            return View(new AdminOrdersViewModel { UnShippedOrders = unShippedOrders, ShippedOrders = shippedOrders});
        }
    }

}
