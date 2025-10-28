using Domain.ValueObjects;

namespace Application.DTOs;

public class BookSeatInputDto
{
    public Guid BusId { get; set; }
    public Guid SeatId { get; set; }
    public Passenger Passenger { get; set; } = new Passenger();
    public string BoardingPoint { get; set; } = string.Empty;
    public string DroppingPoint { get; set; } = string.Empty;
    public bool Confirm { get; set; } = true;
    
    // If true, only update passenger Name and Phone for an already booked seat
    public bool IsUpdate { get; set; }
}
