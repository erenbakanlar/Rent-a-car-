namespace RentACar.Domain.Entities;

/// <summary>
/// Araç entity'si - sistemdeki kiralık araçları temsil eder
/// </summary>
public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;   // Marka (Toyota, BMW vb.)
    public string Model { get; set; } = string.Empty;   // Model (Corolla, 3 Series vb.)
    public int Year { get; set; }                        // Üretim yılı
    public decimal PricePerDay { get; set; }             // Günlük kiralama ücreti
    public CarStatus Status { get; set; } = CarStatus.Available;
    public string? ImageUrl { get; set; }                // Araç görseli URL'i

    // Navigation property
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

public enum CarStatus
{
    Available = 0,   // Müsait
    Rented = 1,      // Kirada
    Maintenance = 2  // Bakımda
}
