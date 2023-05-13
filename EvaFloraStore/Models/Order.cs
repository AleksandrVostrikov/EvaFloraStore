using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EvaFloraStore.Models
{
    public class Order
    {
        [BindNever]
        public Guid OrderId { get; set; }
        [BindNever]
        public ICollection<CartLine>? Lines { get; set; }
        [BindNever]
        public bool Shipped { get; set; } = false;
        [BindNever]
        public bool Archive { get; set; } = false;


        public string Name { get; set; }
        public string Region { get; set; }
        public string Adress { get; set; }
        public string ZIP { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
