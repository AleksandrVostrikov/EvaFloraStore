using EvaFloraStore.Models.ViewModels;
using EvaFloraStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using EvaFloraStore.Repositories.Db;

namespace EvaFloraStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Cart _cart;

        public OrderController(
            IOrderRepository orderRepository,
            Cart cartService)
        {
            _orderRepository = orderRepository;
            _cart = cartService;
        }
        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            order.Lines = _cart.Lines.ToArray();
            order.TotalSum = _cart.ComputeTotalValue();
            await _orderRepository.SaveOrder(order);
            _cart.Clear();
            return View();
        }



    }
}
