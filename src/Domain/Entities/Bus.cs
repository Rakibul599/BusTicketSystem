namespace Domain.Entities;

public class Bus
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CompanyName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int TotalSeats { get; set; }
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();

    // New: store boarding and dropping stop points for this bus
    public ICollection<StopPoint> StopPoints { get; set; } = new List<StopPoint>();

    public int SeatsLeft => Seats.Count(s => s.Status == SeatStatus.Available);

    // Convenience accessors
    public IEnumerable<string> BoardingPoints => StopPoints?.Where(sp => sp.Type == StopType.Boarding).Select(sp => sp.Name) ?? Enumerable.Empty<string>();
    public IEnumerable<string> DroppingPoints => StopPoints?.Where(sp => sp.Type == StopType.Dropping).Select(sp => sp.Name) ?? Enumerable.Empty<string>();
}
