using Application.Interfaces;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _ctx;

    public UnitOfWork(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<int> CommitAsync(CancellationToken ct = default)
    {
        return await _ctx.SaveChangesAsync(ct);
    }
}
