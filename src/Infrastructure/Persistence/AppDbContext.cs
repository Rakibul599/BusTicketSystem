using Domain.Entities;
using Domain.ValueObjects; // Add this line
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

    public DbSet<Bus> Buses { get; set; } = null!;
    public DbSet<StopPoint> StopPoints { get; set; } = null!;
    public DbSet<Seat> Seats { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bus>().HasKey(b => b.Id);
        modelBuilder.Entity<Seat>().HasKey(s => s.Id);
        modelBuilder.Entity<Ticket>().HasKey(t => t.Id);

        modelBuilder.Entity<Bus>()
            .HasMany(b => b.Seats)
            .WithOne(s => s.Bus)
            .HasForeignKey(s => s.BusId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Bus>()
            .HasMany(b => b.StopPoints)
            .WithOne(sp => sp.Bus)
            .HasForeignKey(sp => sp.BusId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Seat>()
            .HasOne(s => s.Ticket)
            .WithOne(t => t.Seat)
            .HasForeignKey<Ticket>(t => t.SeatId);
    }
}
