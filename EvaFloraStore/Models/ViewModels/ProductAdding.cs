using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EvaFloraStore.Models.ViewModels
{
    public class ProductAdding
    {
        public Product? Product { get; set; }
        
        [ValidateNever]
        public IEnumerable<Category>? Categories { get; set; }
        [ValidateNever]
        public IFormFile? Image { get; set; }
    }
}
