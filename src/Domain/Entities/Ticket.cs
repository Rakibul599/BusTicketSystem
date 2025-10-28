using Domain.ValueObjects;

namespace Domain.Entities;

public class Ticket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SeatId { get; set; }
    public Seat? Seat { get; set; }
    public Passenger Passenger { get; set; } = new Passenger();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Confirmed { get; set; }
    public string BoardingPoint { get; set; } = string.Empty;
    public string DroppingPoint { get; set; } = string.Empty;
}
