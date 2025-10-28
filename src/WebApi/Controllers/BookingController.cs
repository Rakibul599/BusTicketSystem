using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly BookingService _booking;

    public BookingController(BookingService booking)
    {
        _booking = booking;
    }

    [HttpGet("{busId}/seats")]
    public async Task<IActionResult> GetSeats(Guid busId)
    {
        var seats = await _booking.GetSeatPlanAsync(busId);
        return Ok(seats);
    }

    [HttpPost("book")]
    public async Task<IActionResult> Book([FromBody] BookSeatInputDto dto)
    {
        var (success, message) = await _booking.BookSeatAsync(dto);
        if (!success) return BadRequest(new { message });
        return Ok(new { message });
    }
}
