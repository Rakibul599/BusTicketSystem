using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusController : ControllerBase
{
    private readonly SearchService _search;
    private readonly BusService _busService;
    private readonly Application.Interfaces.IBusRepository _busRepo;
    private readonly BookingService _bookingService;

    public BusController(SearchService search, BusService busService, Application.Interfaces.IBusRepository busRepo, BookingService bookingService)
    {
        _search = search;
        _busService = busService;
        _busRepo = busRepo;
        _bookingService = bookingService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string from, [FromQuery] string to, [FromQuery] DateTime date)
    {
        var result = await _search.SearchAvailableBusesAsync(from, to, date);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Application.DTOs.CreateBusDto dto)
    {
        var bus = await _busService.CreateBusAsync(dto);
        if (bus == null) return BadRequest("Could not create bus");

        return CreatedAtAction(nameof(GetById), new { id = bus.Id }, new { bus.Id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        // reuse search repo via search service by calling repo through search service is not available,
        // so call repository indirectly: use search to search by id via SearchService or create a small lookup
        // For simplicity, use SearchService's repo through reflection not ideal; better to inject IBusRepository but keep simple.
        // We'll ask client to use repository directly if needed. For now, return NotImplemented.
        var bus = await _busRepo.GetByIdAsync(id);
        if (bus == null) return NotFound();

        var dto = new Application.DTOs.AvailableBusDto
        {
            BusId = bus.Id,
            CompanyName = bus.CompanyName,
            Name = bus.Name,
            StartTime = bus.StartTime,
            ArrivalTime = bus.ArrivalTime,
            SeatsLeft = bus.SeatsLeft,
            Price = 0m,
            BoardingPoints = bus.StopPoints.Where(sp => sp.Type == Domain.Entities.StopType.Boarding).Select(sp => sp.Name).ToList(),
            DroppingPoints = bus.StopPoints.Where(sp => sp.Type == Domain.Entities.StopType.Dropping).Select(sp => sp.Name).ToList()
        };

        return Ok(dto);
    }
}
