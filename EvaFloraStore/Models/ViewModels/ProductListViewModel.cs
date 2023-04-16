namespace EvaFloraStore.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public int ProductsCount { get; set; }
        public string CurrentCategory { get; set; }

        public PageDividerViewModel PageDivider { get; set; }

    }

    public class ProductViewModel
    {
        public Product DispalyedProduct { get; set; }
        public int PositionInList { get; set; }
    }
}
