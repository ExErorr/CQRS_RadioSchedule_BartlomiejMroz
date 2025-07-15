namespace BroadcastBoard.Domain.Entities;

public class Show
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Presenter { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int DurationMinutes { get; set; }

    public DateTime EndTime => StartTime.AddMinutes(DurationMinutes);
}