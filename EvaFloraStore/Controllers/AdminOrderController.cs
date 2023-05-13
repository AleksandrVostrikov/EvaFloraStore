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
            var fullOrders = (await _orderRepository.GetOrders()).Where(o => o.Archive == false);
            var unShippedOrders = fullOrders.Where(o => o.Shipped == false);
            var shippedOrders = fullOrders.Where(o => o.Shipped == true);
            return View(new AdminOrdersViewModel { UnShippedOrders = unShippedOrders, ShippedOrders = shippedOrders});
        }

        public async Task<IActionResult> ChangeOrderStatus(Guid orderId)
        {
            if (orderId != Guid.Empty)
            {
                await _orderRepository.UpdateOrderStatus(orderId);
            }
            return RedirectToAction("GetOrders");
        }
    }

}
