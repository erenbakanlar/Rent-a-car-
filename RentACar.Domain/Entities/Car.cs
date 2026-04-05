namespace RentACar.Domain.Entities;

public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public CarStatus Status { get; set; } = CarStatus.Available;
    public CarCategory Category { get; set; } = CarCategory.Economy;
    public string? ImageUrl { get; set; }
    public int Seats { get; set; } = 5;
    public string? FuelType { get; set; }
    public string? Transmission { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

public enum CarStatus
{
    Available = 0,
    Rented = 1,
    Maintenance = 2
}

public enum CarCategory
{
    Economy = 0,
    Compact = 1,
    SUV = 2,
    Premium = 3,
    Van = 4
}
