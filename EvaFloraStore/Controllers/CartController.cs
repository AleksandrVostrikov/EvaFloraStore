using EvaFloraStore.Models;
using EvaFloraStore.Models.ViewModels;
using EvaFloraStore.Repositories.Db;
using Microsoft.AspNetCore.Mvc;

namespace EvaFloraStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IEvaStoreRepository _evaStoreRepository;
        public Cart Cart { get; set; }

        public CartController(
            IEvaStoreRepository evaStoreRepository,
            Cart cartService)
        {
            _evaStoreRepository = evaStoreRepository;
            Cart = cartService;
        }
        public async Task<IActionResult> AddToCart(Guid id, string returnUrl)
        {
            Product product = await _evaStoreRepository.GetProductAsync(id);
            TempData["ProductAddToChart"] = $"{product.Name} добавлен в корзину";
            Cart.AddItem(product, 1);
            return Redirect(returnUrl);
        }

        public IActionResult GetCart(string returnUrl)
        {
            if (returnUrl == null)
            {
                returnUrl = TempData["CartReturnUrl"]?.ToString();
            }
            TempData["CartReturnUrl"] = returnUrl;
            return View(new CartView
            {
                Cart = Cart,
                ReturnUrl = returnUrl
            });
        }

        public IActionResult RemoveLine(Guid productId)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
            cl.Product.Id == productId).Product);
            return RedirectToAction("GetCart");
        }

        public async Task<IActionResult> ScriptUpdateCart(Guid productId, int quantity)
        {
            var product = await _evaStoreRepository.GetProductAsync(productId);
            Cart.ChangeQuantity(product, quantity);
            var total = Cart.ComputeTotalValue().ToString("N2");
            var subtotal = (product.Price * Cart.Lines.First(l => l.Product.Id == productId).Quantity).ToString("N2");
            return Json(new { subtotal, total });
        }
    }
}
