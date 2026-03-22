using RentACar.Domain.Entities;

namespace RentACar.Application.DTOs;

/// <summary>
/// Rezervasyon listeleme için kullanılan DTO
/// </summary>
public class BookingDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int CarId { get; set; }
    public string CarBrand { get; set; } = string.Empty;
    public string CarModel { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Yeni rezervasyon oluşturmak için kullanılan DTO
/// </summary>
public class CreateBookingDto
{
    public int CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

/// <summary>
/// Rezervasyon durumu güncellemek için kullanılan DTO
/// </summary>
public class UpdateBookingStatusDto
{
    public BookingStatus Status { get; set; }
}
