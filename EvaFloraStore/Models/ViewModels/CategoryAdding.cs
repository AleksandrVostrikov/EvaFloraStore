using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EvaFloraStore.Models.ViewModels
{
    public class CategoryAdding
    {
        [ValidateNever] public IEnumerable<Category> Categories { get; set; }
        public Category Category { get; set; }
        [ValidateNever] public string ReturnUrl { get; set; }
    }
}
