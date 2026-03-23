using RentACar.Domain.Entities;

namespace RentACar.Application.DTOs;

public class CarDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int Seats { get; set; }
    public string? FuelType { get; set; }
    public string? Transmission { get; set; }
}

public class CreateCarDto
{
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
}

public class UpdateCarDto : CreateCarDto
{
    public int Id { get; set; }
}
