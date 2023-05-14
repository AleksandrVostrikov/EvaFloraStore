namespace EvaFloraStore.Models.ViewModels
{
    public class AdminOrdersViewModel
    {
        public IEnumerable<Order> UnShippedOrders { get; set; }
        public IEnumerable<Order> ShippedOrders { get; set; }
        public decimal UnshippedSum { get; set; }
        public decimal ShippedSum { get; set;}

    }
}
