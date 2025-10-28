using Domain.Entities;

namespace Application.Interfaces;

public interface IBusRepository
{
    Task<Bus?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Bus>> SearchAsync(string from, string to, DateTime date, CancellationToken ct = default);
    Task AddAsync(Bus bus, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
