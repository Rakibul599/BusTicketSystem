using Domain.Entities;

namespace Application.DTOs;

public class SeatDto
{
    public Guid SeatId { get; set; }
    public int Number { get; set; }
    public string Row { get; set; } = string.Empty;
    public SeatStatus Status { get; set; }
}
