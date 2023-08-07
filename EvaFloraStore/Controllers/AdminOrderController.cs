using EvaFloraStore.Models.ViewModels;
using EvaFloraStore.Repositories.Db;
using EvaFloraStore.Repositories.EmailHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaFloraStore.Controllers
{
    [Authorize]
    public class AdminOrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailHandler _emailHandler;

        public AdminOrderController(
            IOrderRepository orderRepository,
            IEmailHandler emailHandler
            )
        {
            _orderRepository = orderRepository;
            _emailHandler = emailHandler;
        }

        public async Task<IActionResult> GetOrders()
        {
            var fullOrders = (await _orderRepository.GetOrders()).Where(o => o.Archive == false);
            var unShippedOrders = fullOrders.Where(o => o.Shipped == false);
            var unShipрedSum = unShippedOrders.Sum(o => o.TotalSum);
            var shippedOrders = fullOrders.Where(o => o.Shipped == true);
            var shipрedSum = shippedOrders.Sum(o => o.TotalSum);
            return View(new AdminOrdersViewModel { UnShippedOrders = unShippedOrders, 
                ShippedOrders = shippedOrders, ShippedSum = shipрedSum, UnshippedSum = unShipрedSum});
        }

        public async Task<IActionResult> ChangeOrderStatus(Guid orderId)
        {
            if (orderId != Guid.Empty)
            {
                await _orderRepository.UpdateOrderStatus(orderId);
            }
            return RedirectToAction("GetOrders");
        }
        public async Task<IActionResult> GetArchive()
        {
            var Orders = (await _orderRepository.GetOrders()).Where(o => o.Archive == true);
            return View(Orders);
        }

        public async Task<IActionResult> OrderProcessing(Guid orderId)
        {
            var order = (await _orderRepository.GetOrders()).FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                return View(new OrderProcessingViewModel
                {
                    Order = order,
                    AdressLine = $"{order.ZIP}, {order.Region}, {order.Adress}"
                });
            }
            else
            {
                return RedirectToAction("GetOrders");
            }
        }

        [HttpPost]
        public async Task<IActionResult> OrderProcessing(OrderProcessingViewModel model, string returnUrl)
        {
            var order = await _orderRepository.GetOrder(model.Order.OrderId);
            order.Shipping = model.Order.Shipping;
            order.Track = model.Order.Track;
            await _orderRepository.UpdateOrder(order);

            return Redirect(returnUrl);
        }
        public async Task<IActionResult> SendEmail(Guid orderId, string returnUrl)
        {
            var order = await _orderRepository.GetOrder(orderId);
            await _emailHandler.SendOrderConfirmationEmail(order.Email, order.TotalSum, order.Shipping, order.Track);
            order.IsEmail = true;
            await _orderRepository.UpdateOrder(order);
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> ChangeArchiveStatus(Guid orderId)
        {
            if (orderId != Guid.Empty)
            {
                await _orderRepository.UpdateArchiveStatus(orderId);
            }
            return RedirectToAction("GetOrders");
        }
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            if (orderId != Guid.Empty)
            {
                await _orderRepository.DeleteOrder(orderId);
            }
            return RedirectToAction("GetOrders");
        }

    }

}
