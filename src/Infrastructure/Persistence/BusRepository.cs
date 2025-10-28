using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class BusRepository : IBusRepository
{
    private readonly AppDbContext _ctx;

    public BusRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Bus?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _ctx.Buses
            .Include(b => b.Seats)
                .ThenInclude(s => s.Ticket)
            .Include(b => b.StopPoints)
            .FirstOrDefaultAsync(b => b.Id == id, ct);
    }

    public async Task<IEnumerable<Bus>> SearchAsync(string from, string to, DateTime date, CancellationToken ct = default)
    {
        return await _ctx.Buses
            .Include(b => b.Seats)
            .Include(b => b.StopPoints)
            .Where(b => b.From.ToLower() == from.ToLower() && b.To.ToLower() == to.ToLower()
                        && b.StartTime.Date == date.Date)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Bus bus, CancellationToken ct = default)
    {
        await _ctx.Buses.AddAsync(bus, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _ctx.SaveChangesAsync(ct);
    }
}
