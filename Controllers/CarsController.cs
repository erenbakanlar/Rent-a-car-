using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Application.DTOs;
using RentACar.Application.Interfaces;
using RentACar.Domain.Entities;

namespace RentACar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarRepository _carRepository;

    public CarsController(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    /// <summary>
    /// Tüm araçları listeler (herkese açık)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cars = await _carRepository.GetAllAsync();
        var result = cars.Select(MapToDto);
        return Ok(result);
    }

    /// <summary>
    /// Sadece müsait araçları listeler
    /// </summary>
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable()
    {
        var cars = await _carRepository.GetAvailableCarsAsync();
        return Ok(cars.Select(MapToDto));
    }

    /// <summary>
    /// ID'ye göre araç getirir
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car is null) return NotFound(new { message = "Araç bulunamadı." });
        return Ok(MapToDto(car));
    }

    /// <summary>
    /// Yeni araç ekler (sadece Admin)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCarDto dto)
    {
        var car = new Car
        {
            Brand = dto.Brand,
            Model = dto.Model,
            Year = dto.Year,
            PricePerDay = dto.PricePerDay,
            Status = dto.Status,
            Category = dto.Category,
            ImageUrl = dto.ImageUrl,
            Seats = dto.Seats,
            FuelType = dto.FuelType,
            Transmission = dto.Transmission
        };

        var created = await _carRepository.AddAsync(car);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, MapToDto(created));
    }

    /// <summary>
    /// Araç bilgilerini günceller (sadece Admin)
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCarDto dto)
    {
        if (id != dto.Id) return BadRequest(new { message = "ID uyuşmazlığı." });

        var car = await _carRepository.GetByIdAsync(id);
        if (car is null) return NotFound(new { message = "Araç bulunamadı." });

        car.Brand = dto.Brand;
        car.Model = dto.Model;
        car.Year = dto.Year;
        car.PricePerDay = dto.PricePerDay;
        car.Status = dto.Status;
        car.Category = dto.Category;
        car.ImageUrl = dto.ImageUrl;
        car.Seats = dto.Seats;
        car.FuelType = dto.FuelType;
        car.Transmission = dto.Transmission;

        await _carRepository.UpdateAsync(car);
        return NoContent();
    }

    /// <summary>
    /// Araç siler (sadece Admin)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car is null) return NotFound(new { message = "Araç bulunamadı." });

        await _carRepository.DeleteAsync(id);
        return NoContent();
    }

    // Entity -> DTO dönüşümü
    private static CarDto MapToDto(Car car) => new()
    {
        Id = car.Id,
        Brand = car.Brand,
        Model = car.Model,
        Year = car.Year,
        PricePerDay = car.PricePerDay,
        Status = car.Status.ToString(),
        Category = car.Category.ToString(),
        ImageUrl = car.ImageUrl,
        Seats = car.Seats,
        FuelType = car.FuelType,
        Transmission = car.Transmission
    };
}
