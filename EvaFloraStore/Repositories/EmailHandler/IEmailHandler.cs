namespace EvaFloraStore.Repositories.EmailHandler
{
    public interface IEmailHandler
    {
        Task SendSuccesOrderEmail(string email);
        Task SendOrderConfirmationEmail (string email, decimal cost, decimal shipping, string track);
    }
}
