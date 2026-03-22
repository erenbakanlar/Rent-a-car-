using Microsoft.AspNetCore.Mvc;
using RentACar.Application.DTOs;
using RentACar.Domain.Entities;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RentACar.MVC.Controllers;

/// <summary>
/// Admin paneli controller'ı - araç ve rezervasyon yönetimi
/// Tüm işlemler jQuery AJAX ile API'ye iletilir
/// </summary>
public class AdminController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private static readonly JsonSerializerOptions _jsonOptions =
        new() { PropertyNameCaseInsensitive = true };

    public AdminController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient GetAuthClient()
    {
        var client = _httpClientFactory.CreateClient("RentACarAPI");
        var token = HttpContext.Session.GetString("JwtToken");
        if (!string.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    private bool IsAdmin() =>
        HttpContext.Session.GetString("UserRoles")?.Contains("Admin") == true;

    // ─── Dashboard ───────────────────────────────────────────────────────────

    public async Task<IActionResult> Index()
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");

        var client = GetAuthClient();
        var carsTask = client.GetAsync("/api/cars");
        var bookingsTask = client.GetAsync("/api/bookings");
        await Task.WhenAll(carsTask, bookingsTask);

        var cars = new List<CarDto>();
        var bookings = new List<BookingDto>();

        if (carsTask.Result.IsSuccessStatusCode)
            cars = JsonSerializer.Deserialize<List<CarDto>>(
                await carsTask.Result.Content.ReadAsStringAsync(), _jsonOptions) ?? cars;

        if (bookingsTask.Result.IsSuccessStatusCode)
            bookings = JsonSerializer.Deserialize<List<BookingDto>>(
                await bookingsTask.Result.Content.ReadAsStringAsync(), _jsonOptions) ?? bookings;

        ViewBag.TotalCars = cars.Count;
        ViewBag.AvailableCars = cars.Count(c => c.Status == "Available");
        ViewBag.RentedCars = cars.Count(c => c.Status == "Rented");
        ViewBag.TotalBookings = bookings.Count;
        ViewBag.PendingBookings = bookings.Count(b => b.Status == "Pending");
        ViewBag.TotalRevenue = bookings
            .Where(b => b.Status != "Cancelled")
            .Sum(b => b.TotalPrice);

        return View();
    }

    // ─── Araç Yönetimi ───────────────────────────────────────────────────────

    public async Task<IActionResult> Cars()
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");

        var client = GetAuthClient();
        var response = await client.GetAsync("/api/cars");
        var cars = new List<CarDto>();

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            cars = JsonSerializer.Deserialize<List<CarDto>>(json, _jsonOptions) ?? cars;
        }

        return View(cars);
    }

    [HttpGet]
    public IActionResult CreateCar()
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");
        return View();
    }

    // AJAX: Araç ekle
    [HttpPost]
    public async Task<IActionResult> CreateCarAjax([FromBody] CreateCarDto dto)
    {
        if (!IsAdmin()) return Json(new { success = false, message = "Yetkisiz erişim." });

        var client = GetAuthClient();
        var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/cars", content);

        if (response.IsSuccessStatusCode)
            return Json(new { success = true, message = "Araç başarıyla eklendi." });

        var err = await response.Content.ReadAsStringAsync();
        return Json(new { success = false, message = "Araç eklenemedi." });
    }

    // AJAX: Araç güncelle
    [HttpPost]
    public async Task<IActionResult> UpdateCarAjax([FromBody] UpdateCarDto dto)
    {
        if (!IsAdmin()) return Json(new { success = false, message = "Yetkisiz erişim." });

        var client = GetAuthClient();
        var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"/api/cars/{dto.Id}", content);

        if (response.IsSuccessStatusCode)
            return Json(new { success = true, message = "Araç güncellendi." });

        return Json(new { success = false, message = "Güncelleme başarısız." });
    }

    // AJAX: Araç sil
    [HttpPost]
    public async Task<IActionResult> DeleteCarAjax([FromBody] IdDto dto)
    {
        if (!IsAdmin()) return Json(new { success = false, message = "Yetkisiz erişim." });

        var client = GetAuthClient();
        var response = await client.DeleteAsync($"/api/cars/{dto.Id}");

        if (response.IsSuccessStatusCode)
            return Json(new { success = true });

        return Json(new { success = false, message = "Silme işlemi başarısız." });
    }

    // ─── Rezervasyon Yönetimi ─────────────────────────────────────────────────

    public async Task<IActionResult> Bookings()
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");

        var client = GetAuthClient();
        var response = await client.GetAsync("/api/bookings");
        var bookings = new List<BookingDto>();

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            bookings = JsonSerializer.Deserialize<List<BookingDto>>(json, _jsonOptions) ?? bookings;
        }

        return View(bookings);
    }

    // AJAX: Rezervasyon durumu güncelle
    [HttpPost]
    public async Task<IActionResult> UpdateBookingStatusAjax([FromBody] UpdateStatusDto dto)
    {
        if (!IsAdmin()) return Json(new { success = false, message = "Yetkisiz erişim." });

        var client = GetAuthClient();
        var payload = new { Status = dto.Status };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await client.PatchAsync($"/api/bookings/{dto.Id}/status", content);

        if (response.IsSuccessStatusCode)
            return Json(new { success = true });

        return Json(new { success = false, message = "Durum güncellenemedi." });
    }
}

// Yardımcı DTO'lar (sadece MVC katmanında kullanılır)
public record IdDto(int Id);
public record UpdateStatusDto(int Id, int Status);
