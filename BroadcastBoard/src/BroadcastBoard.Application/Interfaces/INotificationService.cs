using System.Threading.Tasks;

namespace BroadcastBoard.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task NotifyAsync(string message);
    }
}

