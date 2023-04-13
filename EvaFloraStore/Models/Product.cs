using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaFloraStore.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Укажите название товара")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Добавьте описание товара")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше нуля")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsVisible { get; set; } = true;
    }
}
