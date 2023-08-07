using EvaFloraStore.Infrastructure;
using Newtonsoft.Json;

namespace EvaFloraStore.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            SessionCart cart = session?.GetJson<SessionCart>("cart") ?? new SessionCart();
            if (session != null)
            {
                cart.Session = session;
            }
            return cart;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session?.SetJson("cart", this);
            
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session?.SetJson("cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session?.SetJson("cart", this);
        }

        public override void ChangeQuantity(Product product, int quantity)
        {
            base.ChangeQuantity(product, quantity);
            Session?.SetJson("cart", this);
        }
    }
}
