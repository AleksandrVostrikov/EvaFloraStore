namespace EvaFloraStore.Models.ViewModels
{
    public class ProductListViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new();
        public int ProductsCount { get; set; }
    }
}
