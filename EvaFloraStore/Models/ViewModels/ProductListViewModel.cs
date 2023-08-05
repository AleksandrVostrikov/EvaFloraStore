namespace EvaFloraStore.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product>? Products { get; set; }
        public int ProductsCount { get; set; }
        public string? CurrentCategory { get; set; }

        public PageDividerViewModel PageDivider { get; set; }

    }
}
