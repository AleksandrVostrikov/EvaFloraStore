using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

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
        [BindNever]
        public decimal TotalSum { get; set; }

        [Required(ErrorMessage = "Введите ФИО")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите Область/Край")]
        public string Region { get; set; }
        [Required(ErrorMessage = "Введите Адрес")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Введите индекс")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Индекс должен состоять из 6 цифр")]
        public string ZIP { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Введите верный Email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$", ErrorMessage = "Номер телефона указан неверно!")]
        public string Phone { get; set; }
    }
}
