using System.ComponentModel.DataAnnotations.Schema;

namespace EvaFloraStore.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public Guid ProductId { get; set; } 
        public string ImageUrl { get; set; }
    }
}
