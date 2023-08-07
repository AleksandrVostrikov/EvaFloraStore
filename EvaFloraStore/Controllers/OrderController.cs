using EvaFloraStore.Models;
using Microsoft.AspNetCore.Mvc;
using EvaFloraStore.Repositories.Db;
using EvaFloraStore.Repositories.EmailHandler;

namespace EvaFloraStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Cart _cart;
        private readonly IEmailHandler _emailHandler;

        public OrderController(
            IOrderRepository orderRepository,
            Cart cartService,
            IEmailHandler emailHandler)
        {
            _orderRepository = orderRepository;
            _cart = cartService;
            _emailHandler = emailHandler;
        }
        public IActionResult Checkout()
        {
            return View(new Order());
        }
        public IActionResult OrderSuccess()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                order.TotalSum = _cart.ComputeTotalValue();
                await _orderRepository.SaveOrder(order);
                await _emailHandler.SendSuccesOrderEmail(order.Email);
                _cart.Clear();
                return RedirectToAction("OrderSuccess");
            }
            else
            {
                return View();
            }
                
        }
    }
}
