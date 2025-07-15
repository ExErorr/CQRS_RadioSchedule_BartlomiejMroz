using BroadcastBoard.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace BroadcastBoard.Infrastructure.Services;

public class DummyNotificationService : INotificationService
{
    public Task NotifyAsync(string message)
    {
        Console.WriteLine("Notification: " + message);
        return Task.CompletedTask;
    }
}
