using Microsoft.AspNetCore.Mvc;
using RentACar.Application.DTOs;
using System.Text;
using System.Text.Json;

namespace RentACar.MVC.Controllers;

public class AccountController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private static readonly JsonSerializerOptions _jsonOptions =
        new() { PropertyNameCaseInsensitive = true };

    public AccountController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var client = _httpClientFactory.CreateClient("RentACarAPI");
        var content = new StringContent(
            JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var auth = JsonSerializer.Deserialize<AuthResponseDto>(json, _jsonOptions);

            if (auth is not null)
            {
                // Token ve kullanıcı bilgilerini session'a kaydet
                HttpContext.Session.SetString("JwtToken", auth.Token);
                HttpContext.Session.SetString("UserEmail", auth.Email);
                HttpContext.Session.SetString("UserFullName", auth.FullName);
                HttpContext.Session.SetString("UserRoles",
                    string.Join(",", auth.Roles));

                // Admin ise admin paneline, değilse ana sayfaya yönlendir
                if (auth.Roles.Contains("Admin"))
                    return RedirectToAction("Index", "Admin");

                return RedirectToAction("Index", "Home");
            }
        }

        ViewBag.Error = "E-posta veya şifre hatalı.";
        return View(dto);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var client = _httpClientFactory.CreateClient("RentACarAPI");
        var content = new StringContent(
            JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/auth/register", content);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Kayıt başarılı! Giriş yapabilirsiniz.";
            return RedirectToAction(nameof(Login));
        }

        ViewBag.Error = "Kayıt sırasında bir hata oluştu.";
        return View(dto);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
