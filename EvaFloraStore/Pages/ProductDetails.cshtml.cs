using EvaFloraStore.Models;
using EvaFloraStore.Repositories.Db;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EvaFloraStore.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly IEvaStoreRepository _evaStoreRepository;

        public Product Product { get; set; }
        public string ReturnUrl { get; set; }

        public ProductDetailsModel(IEvaStoreRepository evaStoreRepository)
        {
            _evaStoreRepository = evaStoreRepository;
        }
        
        public async Task OnGet(Guid id, string returnUrl)
        {
            Product = await _evaStoreRepository.GetProductAsync(id);
            ReturnUrl = returnUrl ?? "/";
        }
    }
}
