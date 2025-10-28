using System;
namespace Domain.Entities;

public enum StopType
{
    Boarding = 0,
    Dropping = 1
}

public class StopPoint
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BusId { get; set; }
    public Bus? Bus { get; set; }
    public string Name { get; set; } = string.Empty;
    public StopType Type { get; set; } = StopType.Boarding;
}
