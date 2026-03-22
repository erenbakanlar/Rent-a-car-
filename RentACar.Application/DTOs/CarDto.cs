using RentACar.Domain.Entities;

namespace RentACar.Application.DTOs;

/// <summary>
/// Araç listeleme ve detay için kullanılan DTO
/// </summary>
public class CarDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}

/// <summary>
/// Araç oluşturma ve güncelleme için kullanılan DTO
/// </summary>
public class CreateCarDto
{
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public CarStatus Status { get; set; } = CarStatus.Available;
    public string? ImageUrl { get; set; }
}

public class UpdateCarDto : CreateCarDto
{
    public int Id { get; set; }
}
