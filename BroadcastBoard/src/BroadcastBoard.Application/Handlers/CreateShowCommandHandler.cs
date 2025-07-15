using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using BroadcastBoard.Domain.Entities;
using BroadcastBoard.Domain.Common.Interfaces;
using BroadcastBoard.Application.Common.Interfaces;
using BroadcastBoard.Application.Shows.DTO;
using BroadcastBoard.Application.Shows.Exceptions;
using BroadcastBoard.Application.Common.Constants;

namespace BroadcastBoard.Application.Shows.Commands
{
    public class CreateShowCommandHandler : IRequestHandler<CreateShowCommand, ShowDto>
    {
        private readonly IShowRepository _repository;
        private readonly INotificationService _notificationService;

        public CreateShowCommandHandler(IShowRepository repository, INotificationService notificationService)
        {
            _repository = repository;
            _notificationService = notificationService;
        }

        public async Task<ShowDto> Handle(CreateShowCommand request, CancellationToken cancellationToken)
        {
            var shows = await _repository.GetShowsByDateAsync(request.StartTime.Date);
            var newShowStart = request.StartTime;
            var newShowEnd = request.StartTime.AddMinutes(request.DurationMinutes);

            bool hasCollision = shows.Any(s =>
                (newShowStart < s.StartTime.AddMinutes(s.DurationMinutes)) &&
                (newShowEnd > s.StartTime));

            if (hasCollision)
            {
                throw new ShowCollisionException(ExceptionMessages.ShowCollidesWithExisting);
            }

            var newShow = new Show
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Presenter = request.Presenter,
                StartTime = request.StartTime,
                DurationMinutes = request.DurationMinutes
            };

            await _repository.AddAsync(newShow);

            await _notificationService.NotifyAsync($"Nowa audycja: {newShow.Title} o {newShow.StartTime} prowadzona przez {newShow.Presenter}");

            return new ShowDto(newShow.Id, newShow.Title, newShow.Presenter, newShow.StartTime, newShow.DurationMinutes);
        }
    }
}
