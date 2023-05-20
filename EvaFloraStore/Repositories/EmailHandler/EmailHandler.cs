using EvaFloraStore.Infrastructure;
using EvaFloraStore.Models;

namespace EvaFloraStore.Repositories.EmailHandler
{
    public class EmailHandler : IEmailHandler
    {
        private readonly Cart _cart;
        private readonly EmailService _emailService;

        public EmailHandler(Cart cart, EmailService emailService)
        {
            _cart = cart;
            _emailService = emailService;
        }
        public async Task SendSuccesOrderEmail(string email)
        {
            var orderLines = _cart.Lines.ToArray();
            string totalValue = _cart.ComputeTotalValue().ToString();
            string emailMessage = $"Здравствуйте, вы успешно оформили заказ в нашем магазине на сумму {totalValue} руб.{Environment.NewLine}";
            foreach (var line in orderLines)
            {
                emailMessage += $"{line.Product.Name} ---- {line.Quantity} шт. {Environment.NewLine}";
            }
            emailMessage += "Как только я упакую продукцию, то пришлю реквизиты карты для оплаты и полную стоимость с учетом доставки. " +
                "Спасибо за заказ! Если у Вас есть вопросы, то Вы можете задать их в ответном письме.";
            await _emailService.SendEmailAsync(email, "Заказ продукции", emailMessage);
        }
    }
}
