namespace EvaFloraStore.Models.ViewModels
{
    public class AdminOrdersViewModel
    {
        public IEnumerable<Order> UnShippedOrders { get; set; }
        public IEnumerable<Order> ShippedOrders { get; set; }

    }
}
