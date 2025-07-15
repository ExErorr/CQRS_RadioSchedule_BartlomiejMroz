using BroadcastBoard.Application.Shows.DTO;
using MediatR;

namespace BroadcastBoard.Application.Queries
{
    public record GetShowsQuery(DateTime Date) : IRequest<IEnumerable<ShowDto>>;
}

