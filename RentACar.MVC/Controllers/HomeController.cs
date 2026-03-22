using Microsoft.AspNetCore.Mvc;
using RentACar.Application.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RentACar.MVC.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Ana sayfa - müsait araçları listeler
    /// </summary>
    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("RentACarAPI");
        var response = await client.GetAsync("/api/cars/available");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var cars = JsonSerializer.Deserialize<List<CarDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(cars ?? new List<CarDto>());
        }

        return View(new List<CarDto>());
    }
}
