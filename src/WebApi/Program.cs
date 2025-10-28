using Application.Interfaces;
using Application.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// configuration
var conn = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Host=localhost;Database=busdb;Username=postgres;Password=postgres";

// services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(conn));

// register repositories and services
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<SearchService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<BusService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
