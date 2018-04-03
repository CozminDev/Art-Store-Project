namespace ArtStore.Services
{
    public interface INullMailService
    {
        void SendMessage(string to, string email, string subject, string message);
    }
}