using EvaFloraStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EvaFloraStore.Views.Shared.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart _cartService;
        public CartSummaryViewComponent(Cart cartService)
        {
            _cartService = cartService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_cartService);
        }
    }
}
