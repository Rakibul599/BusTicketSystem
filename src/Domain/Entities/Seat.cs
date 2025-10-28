namespace Domain.Entities;

public enum SeatStatus
{
    Available,
    Booked,
    Sold
}

public class Seat
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Number { get; set; }
    public string Row { get; set; } = string.Empty;
    public SeatStatus Status { get; set; } = SeatStatus.Available;
    public Guid BusId { get; set; }
    public Bus? Bus { get; set; }
    public Ticket? Ticket { get; set; }
}
