using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Application.DTOs;
using RentACar.Application.Interfaces;
using RentACar.Domain.Entities;

namespace RentACar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Tüm endpoint'ler giriş gerektirir
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ICarRepository _carRepository;

    public BookingsController(IBookingRepository bookingRepository, ICarRepository carRepository)
    {
        _bookingRepository = bookingRepository;
        _carRepository = carRepository;
    }

    /// <summary>
    /// Tüm rezervasyonları listeler (sadece Admin)
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await _bookingRepository.GetBookingsWithCarAsync();
        return Ok(bookings.Select(MapToDto));
    }

    /// <summary>
    /// Giriş yapan kullanıcının rezervasyonlarını getirir
    /// </summary>
    [HttpGet("my")]
    public async Task<IActionResult> GetMyBookings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);
        return Ok(bookings.Select(MapToDto));
    }

    /// <summary>
    /// ID'ye göre rezervasyon getirir
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        if (booking is null) return NotFound(new { message = "Rezervasyon bulunamadı." });

        // Kullanıcı sadece kendi rezervasyonunu görebilir (Admin hepsini görebilir)
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var isAdmin = User.IsInRole("Admin");

        if (!isAdmin && booking.UserId != userId)
            return Forbid();

        return Ok(MapToDto(booking));
    }

    /// <summary>
    /// Yeni rezervasyon oluşturur
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
    {
        // Araç var mı ve müsait mi?
        var car = await _carRepository.GetByIdAsync(dto.CarId);
        if (car is null) return NotFound(new { message = "Araç bulunamadı." });
        if (car.Status != CarStatus.Available)
            return BadRequest(new { message = "Bu araç şu an müsait değil." });

        // Tarih validasyonu
        if (dto.StartDate >= dto.EndDate)
            return BadRequest(new { message = "Bitiş tarihi başlangıç tarihinden sonra olmalı." });

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var days = (dto.EndDate - dto.StartDate).Days;
        var totalPrice = days * car.PricePerDay;

        var booking = new Booking
        {
            UserId = userId,
            CarId = dto.CarId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            TotalPrice = totalPrice,
            Status = BookingStatus.Pending
        };

        // Araç durumunu güncelle
        car.Status = CarStatus.Rented;
        await _carRepository.UpdateAsync(car);

        var created = await _bookingRepository.AddAsync(booking);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, MapToDto(created));
    }

    /// <summary>
    /// Rezervasyon durumunu günceller (sadece Admin)
    /// </summary>
    [HttpPatch("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateBookingStatusDto dto)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        if (booking is null) return NotFound(new { message = "Rezervasyon bulunamadı." });

        booking.Status = dto.Status;

        // Rezervasyon tamamlandı veya iptal edildi ise araç tekrar müsait olur
        if (dto.Status is BookingStatus.Completed or BookingStatus.Cancelled)
        {
            var car = await _carRepository.GetByIdAsync(booking.CarId);
            if (car is not null)
            {
                car.Status = CarStatus.Available;
                await _carRepository.UpdateAsync(car);
            }
        }

        await _bookingRepository.UpdateAsync(booking);
        return NoContent();
    }

    /// <summary>
    /// Rezervasyon iptal eder (kullanıcı kendi rezervasyonunu iptal edebilir)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        if (booking is null) return NotFound(new { message = "Rezervasyon bulunamadı." });

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var isAdmin = User.IsInRole("Admin");

        if (!isAdmin && booking.UserId != userId)
            return Forbid();

        // Sadece beklemedeki rezervasyonlar iptal edilebilir
        if (booking.Status != BookingStatus.Pending)
            return BadRequest(new { message = "Sadece beklemedeki rezervasyonlar iptal edilebilir." });

        booking.Status = BookingStatus.Cancelled;
        await _bookingRepository.UpdateAsync(booking);

        // Araç tekrar müsait olur
        var car = await _carRepository.GetByIdAsync(booking.CarId);
        if (car is not null)
        {
            car.Status = CarStatus.Available;
            await _carRepository.UpdateAsync(car);
        }

        return NoContent();
    }

    // Entity -> DTO dönüşümü
    private static BookingDto MapToDto(Booking b) => new()
    {
        Id = b.Id,
        UserId = b.UserId,
        CarId = b.CarId,
        CarBrand = b.Car?.Brand ?? string.Empty,
        CarModel = b.Car?.Model ?? string.Empty,
        StartDate = b.StartDate,
        EndDate = b.EndDate,
        TotalPrice = b.TotalPrice,
        Status = b.Status.ToString()
    };
}
