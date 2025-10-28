using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System.Transactions;

namespace Application.Services;

public class BookingService
{
    private readonly IBusRepository _busRepo;
    private readonly IUnitOfWork _uow;

    public BookingService(IBusRepository busRepo, IUnitOfWork uow)
    {
        _busRepo = busRepo;
        _uow = uow;
    }

    public async Task<(bool Success, string Message)> BookSeatAsync(BookSeatInputDto input, CancellationToken ct = default)
    {
        // If we're updating an existing booking, use UpdatePassengerInfoAsync
        if (input.IsUpdate)
        {
            return await UpdatePassengerInfoAsync(input, ct);
        }

        // Validate bus exists and get it with its seats and stop points
        var bus = await _busRepo.GetByIdAsync(input.BusId, ct);
        if (bus == null) return (false, "Bus not found");

        // Validate seat exists and is available
        var seat = bus.Seats.FirstOrDefault(s => s.Id == input.SeatId);
        if (seat == null) return (false, "Seat not found");
        if (seat.Status != SeatStatus.Available) return (false, "Seat is not available");

        // Validate boarding point
        if (string.IsNullOrWhiteSpace(input.BoardingPoint))
            return (false, "Boarding point is required");
        if (!bus.BoardingPoints.Contains(input.BoardingPoint))
            return (false, $"Invalid boarding point. Available points: {string.Join(", ", bus.BoardingPoints)}");

        // Validate dropping point
        if (string.IsNullOrWhiteSpace(input.DroppingPoint))
            return (false, "Dropping point is required");
        if (!bus.DroppingPoints.Contains(input.DroppingPoint))
            return (false, $"Invalid dropping point. Available points: {string.Join(", ", bus.DroppingPoints)}");

        // Validate passenger info
        if (string.IsNullOrWhiteSpace(input.Passenger?.Name))
            return (false, "Passenger name is required");
        if (string.IsNullOrWhiteSpace(input.Passenger?.Phone))
            return (false, "Passenger phone is required");

        // Create ticket and update seat
        var ticket = new Ticket
        {
            Seat = seat,
            Passenger = input.Passenger,
            BoardingPoint = input.BoardingPoint,
            DroppingPoint = input.DroppingPoint,
            Confirmed = input.Confirm
        };

        seat.Ticket = ticket;
        seat.Status = input.Confirm ? SeatStatus.Sold : SeatStatus.Booked;

        // Save using repository + unit of work
        await _busRepo.SaveChangesAsync(ct);
        await _uow.CommitAsync(ct);

        return (true, "Seat booked successfully");
    }

    // New: update only passenger Name and Phone for an already booked seat
    public async Task<(bool Success, string Message)> UpdatePassengerInfoAsync(BookSeatInputDto input, CancellationToken ct = default)
    {
        var bus = await _busRepo.GetByIdAsync(input.BusId, ct);
        if (bus == null) return (false, "Bus not found");

        var seat = bus.Seats.FirstOrDefault(s => s.Id == input.SeatId);
        if (seat == null) return (false, "Seat not found");

        if (seat.Ticket == null)
            return (false, "Seat is not booked yet");

        // Only update Name and Phone as requested
        var passenger = seat.Ticket.Passenger ?? new Domain.ValueObjects.Passenger();
        if (!string.IsNullOrWhiteSpace(input.Passenger.Name)) passenger.Name = input.Passenger.Name;
        if (!string.IsNullOrWhiteSpace(input.Passenger.Phone)) passenger.Phone = input.Passenger.Phone;

        seat.Ticket.Passenger = passenger;

        await _busRepo.SaveChangesAsync(ct);
        await _uow.CommitAsync(ct);

        return (true, "Passenger information updated");
    }

    public async Task<List<SeatDto>> GetSeatPlanAsync(Guid busId, CancellationToken ct = default)
    {
        var bus = await _busRepo.GetByIdAsync(busId, ct);
        if (bus == null) return new List<SeatDto>();

        return bus.Seats.Select(s => new SeatDto
        {
            SeatId = s.Id,
            Number = s.Number,
            Row = s.Row,
            Status = s.Status
        }).ToList();
    }
}
