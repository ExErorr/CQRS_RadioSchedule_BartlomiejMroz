using BroadcastBoard.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace BroadcastBoard.Infrastructure.Services;

public class DummyNotificationService : INotificationService
{
    public bool NotifyCalled { get; private set; } = false;
    public Task NotifyAsync(string message)
    {
        NotifyCalled = true;
        Console.WriteLine("Notification: " + message);
        return Task.CompletedTask;
    }
}
