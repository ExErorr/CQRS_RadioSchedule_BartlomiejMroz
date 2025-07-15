using BroadcastBoard.Application.Shows.DTO;
using MediatR;

namespace BroadcastBoard.Application.Shows.Commands
{
    public record CreateShowCommand(string Title, string Presenter, DateTime StartTime, int DurationMinutes) : IRequest<ShowDto>;
}

