using System;

namespace Application.DTOs;

public class AvailableBusDto
{
    public Guid BusId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int SeatsLeft { get; set; }
    public decimal Price { get; set; } = 0m;
    // New: boarding and dropping points
    public List<string> BoardingPoints { get; set; } = new();
    public List<string> DroppingPoints { get; set; } = new();
}
