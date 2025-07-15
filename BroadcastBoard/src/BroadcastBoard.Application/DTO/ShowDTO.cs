namespace BroadcastBoard.Application.Shows.DTO
{
    public record ShowDto(
    Guid Id,
    string Title,
    string Presenter,
    DateTime StartTime,
    int DurationMinutes);
}

