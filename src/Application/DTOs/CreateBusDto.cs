using System;
using System.Collections.Generic;

namespace Application.DTOs;

public class CreateBusDto
{
    public string CompanyName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int TotalSeats { get; set; }
    public decimal Price { get; set; } = 0m;
    public List<string> BoardingPoints { get; set; } = new();
    public List<string> DroppingPoints { get; set; } = new();
}
