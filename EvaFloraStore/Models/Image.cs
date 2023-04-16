using System.ComponentModel.DataAnnotations.Schema;

namespace EvaFloraStore.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
