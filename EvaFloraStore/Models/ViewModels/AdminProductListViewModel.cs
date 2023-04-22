namespace EvaFloraStore.Models.ViewModels
{
    public class AdminProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string ReturnUrl { get; set; }
    }

}
