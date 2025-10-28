namespace Domain.ValueObjects;

public class Passenger
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Primary Key
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
