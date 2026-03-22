using Microsoft.AspNetCore.Mvc;
using RentACar.Application.DTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RentACar.MVC.Controllers;

public class BookingController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private static readonly JsonSerializerOptions _jsonOptions =
        new() { PropertyNameCaseInsensitive = true };

    public BookingController(IHttpClientFactory httpClientFactory)
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

    /// <summary>
    /// Kullanıcının kendi rezervasyonlarını listeler
    /// </summary>
    public async Task<IActionResult> MyBookings()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("JwtToken")))
            return RedirectToAction("Login", "Account");

        var client = GetAuthClient();
        var response = await client.GetAsync("/api/bookings/my");
        var bookings = new List<BookingDto>();

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            bookings = JsonSerializer.Deserialize<List<BookingDto>>(json, _jsonOptions) ?? bookings;
        }

        return View(bookings);
    }

    /// <summary>
    /// Rezervasyon iptal et (AJAX)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Cancel(int id)
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("JwtToken")))
            return Json(new { success = false, message = "Oturum açmanız gerekiyor." });

        var client = GetAuthClient();
        var response = await client.DeleteAsync($"/api/bookings/{id}");

        if (response.IsSuccessStatusCode)
            return Json(new { success = true });

        var error = await response.Content.ReadAsStringAsync();
        return Json(new { success = false, message = "İptal işlemi başarısız." });
    }
}
