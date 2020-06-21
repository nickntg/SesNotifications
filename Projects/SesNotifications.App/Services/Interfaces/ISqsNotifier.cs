using SesNotifications.App.Models;

namespace SesNotifications.App.Services.Interfaces
{
    public interface ISqsNotifier
    {
        void Notify(string header, string message, SqsConfiguration configuration);
    }
}