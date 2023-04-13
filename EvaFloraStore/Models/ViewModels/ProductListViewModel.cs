namespace EvaFloraStore.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public int ProductsCount { get; set; }

        public PageDividerViewModel PageDivider { get; set; }

    }
}
