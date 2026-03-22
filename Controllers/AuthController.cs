using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentACar.Application.DTOs;

namespace RentACar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    /// <summary>
    /// Yeni kullanıcı kaydı
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser is not null)
            return BadRequest(new { message = "Bu e-posta adresi zaten kayıtlı." });

        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            // FullName Identity'de yok, claim olarak saklıyoruz
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        // FullName'i claim olarak ekle
        await _userManager.AddClaimAsync(user, new Claim("FullName", dto.FullName));

        // Varsayılan rol: User
        if (!await _roleManager.RoleExistsAsync("User"))
            await _roleManager.CreateAsync(new IdentityRole("User"));

        await _userManager.AddToRoleAsync(user, "User");

        return Ok(new { message = "Kayıt başarılı." });
    }

    /// <summary>
    /// Kullanıcı girişi - JWT token döner
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            return Unauthorized(new { message = "E-posta veya şifre hatalı." });

        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);
        var fullName = claims.FirstOrDefault(c => c.Type == "FullName")?.Value ?? user.Email!;

        var token = GenerateJwtToken(user, roles, fullName);

        return Ok(new AuthResponseDto
        {
            Token = token.Token,
            Email = user.Email!,
            FullName = fullName,
            Roles = roles,
            Expiration = token.Expiration
        });
    }

    /// <summary>
    /// Admin rolü oluşturma (sadece geliştirme amaçlı)
    /// </summary>
    [HttpPost("create-admin")]
    public async Task<IActionResult> CreateAdmin([FromBody] RegisterDto dto)
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

        var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddClaimAsync(user, new Claim("FullName", dto.FullName));
        await _userManager.AddToRoleAsync(user, "Admin");

        return Ok(new { message = "Admin kullanıcı oluşturuldu." });
    }

    // JWT token üretimi
    private (string Token, DateTime Expiration) GenerateJwtToken(
        IdentityUser user, IList<string> roles, string fullName)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
        var expiration = DateTime.UtcNow.AddHours(
            int.Parse(jwtSettings["ExpirationHours"] ?? "24"));

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new("FullName", fullName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Rolleri claim olarak ekle
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiration);
    }
}
