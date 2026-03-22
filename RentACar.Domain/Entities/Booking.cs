namespace RentACar.Domain.Entities;

/// <summary>
/// Kiralama rezervasyonu entity'si
/// </summary>
public class Booking
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;  // Identity User ID (string)
    public int CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    // Navigation properties
    public Car Car { get; set; } = null!;
}

public enum BookingStatus
{
    Pending = 0,    // Beklemede
    Confirmed = 1,  // Onaylandı
    Cancelled = 2,  // İptal edildi
    Completed = 3   // Tamamlandı
}
