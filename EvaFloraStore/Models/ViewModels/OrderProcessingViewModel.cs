namespace EvaFloraStore.Models.ViewModels
{
    public class OrderProcessingViewModel
    {
        public Order Order { get; set; }
        public string AdressLine { get; set; } = "адрес не указан";
    }
}
