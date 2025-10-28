using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System.Linq;

namespace Application.Services;

public class SearchService
{
    private readonly IBusRepository _busRepo;

    public SearchService(IBusRepository busRepo)
    {
        _busRepo = busRepo;
    }

    public async Task<List<AvailableBusDto>> SearchAvailableBusesAsync(string from, string to, DateTime journeyDate, CancellationToken ct = default)
    {
        var buses = await _busRepo.SearchAsync(from, to, journeyDate, ct);

        return buses.Select(b => new AvailableBusDto
        {
            BusId = b.Id,
            CompanyName = b.CompanyName,
            Name = b.Name,
            StartTime = b.StartTime,
            ArrivalTime = b.ArrivalTime,
            SeatsLeft = b.SeatsLeft,
            Price = 500 // placeholder price; in real app price comes from entity
            ,
            BoardingPoints = b.StopPoints.Where(sp => sp.Type == Domain.Entities.StopType.Boarding).Select(sp => sp.Name).ToList(),
            DroppingPoints = b.StopPoints.Where(sp => sp.Type == Domain.Entities.StopType.Dropping).Select(sp => sp.Name).ToList()
        }).ToList();
    }
}
