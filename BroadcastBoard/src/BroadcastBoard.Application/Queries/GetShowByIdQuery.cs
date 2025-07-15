using BroadcastBoard.Application.Shows.DTO;
using MediatR;

namespace BroadcastBoard.Application.Shows.Queries
{
    public record GetShowByIdQuery(Guid Id) : IRequest<ShowDto>;
}

