namespace RentACar.Domain.Entities;

/// <summary>
/// Araç entity'si - sistemdeki kiralık araçları temsil eder
/// </summary>
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
    public string? FuelType { get; set; }   // Benzin, Dizel, Elektrik, Hibrit
    public string? Transmission { get; set; } // Manuel, Otomatik

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
    Economy = 0,    // Ekonomi
    Compact = 1,    // Kompakt
    SUV = 2,        // SUV
    Premium = 3,    // Premium
    Van = 4         // Van / Minivan
}
