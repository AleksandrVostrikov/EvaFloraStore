namespace EvaFloraStore.Repositories.EmailHandler
{
    public interface IEmailHandler
    {
        Task SendSuccesOrderEmail(string email);
    }
}
