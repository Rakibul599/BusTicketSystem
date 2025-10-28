using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class BusService
{
    private readonly IBusRepository _busRepo;
    private readonly IUnitOfWork _uow;

    public BusService(IBusRepository busRepo, IUnitOfWork uow)
    {
        _busRepo = busRepo;
        _uow = uow;
    }

    public async Task<Bus?> CreateBusAsync(CreateBusDto dto, CancellationToken ct = default)
    {
        // Build bus entity
        var bus = new Bus
        {
            CompanyName = dto.CompanyName,
            Name = dto.Name,
            From = dto.From,
            To = dto.To,
            StartTime = dto.StartTime,
            ArrivalTime = dto.ArrivalTime,
            TotalSeats = dto.TotalSeats
        };

        // Create seats
        for (int i = 1; i <= dto.TotalSeats; i++)
        {
            bus.Seats.Add(new Seat
            {
                Number = i,
                Row = "A",
                Status = SeatStatus.Available,
                Bus = bus
            });
        }

        // Add stop points
        foreach (var bp in dto.BoardingPoints.Distinct())
        {
            bus.StopPoints.Add(new StopPoint { Name = bp, Type = StopType.Boarding, Bus = bus });
        }

        foreach (var dp in dto.DroppingPoints.Distinct())
        {
            bus.StopPoints.Add(new StopPoint { Name = dp, Type = StopType.Dropping, Bus = bus });
        }

        await _busRepo.AddAsync(bus, ct);
        await _uow.CommitAsync(ct);

        return bus;
    }
}
