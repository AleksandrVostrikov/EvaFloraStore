using EvaFloraStore.Models;
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

        public IActionResult GetCart(/*string returnUrl*/)
        {
            return View(Cart);
        }
    }
}
