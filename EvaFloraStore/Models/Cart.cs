using Microsoft.CodeAnalysis.VisualBasic;

namespace EvaFloraStore.Models
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = Lines
                .Where(p => p.Product.Id == product.Id)
                .FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product product) =>
            Lines.RemoveAll(l => l.Product.Id == product.Id);

        public decimal ComputeTotalValue() => Lines.Sum(l => l.Quantity * l.Product.Price);

        public virtual void Clear() => Lines.Clear();

        public virtual void ChangeQuantity(Product product, int quantity)
        {
            CartLine line = Lines
                .Where(p => p.Product.Id == product.Id)
                .FirstOrDefault();
            if (line != null)
            {
                line.Quantity = quantity;
            }
        }

    }


}
