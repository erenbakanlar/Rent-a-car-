using Microsoft.EntityFrameworkCore;
using RentACar.Infrastructure.Data;
using RentACar.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "RentACar API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT token girin: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    
    if (!db.Cars.Any())
    {
        db.Cars.AddRange(
            new RentACar.Domain.Entities.Car { Brand = "Fiat", Model = "Egea", Year = 2023, PricePerDay = 450, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Economy, ImageUrl = "https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?w=800", Seats = 5, FuelType = "Benzin", Transmission = "Manuel" },
            new RentACar.Domain.Entities.Car { Brand = "Renault", Model = "Clio", Year = 2023, PricePerDay = 420, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Economy, ImageUrl = "https://images.unsplash.com/photo-1583121274602-3e2820c69888?w=800", Seats = 5, FuelType = "Benzin", Transmission = "Manuel" },
            new RentACar.Domain.Entities.Car { Brand = "Hyundai", Model = "i20", Year = 2023, PricePerDay = 440, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Economy, ImageUrl = "https://images.unsplash.com/photo-1552519507-da3b142c6e3d?w=800", Seats = 5, FuelType = "Benzin", Transmission = "Otomatik" },
            new RentACar.Domain.Entities.Car { Brand = "Volkswagen", Model = "Golf", Year = 2023, PricePerDay = 650, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Compact, ImageUrl = "https://images.unsplash.com/photo-1552519507-da3b142c6e3d?w=800", Seats = 5, FuelType = "Dizel", Transmission = "Otomatik" },
            new RentACar.Domain.Entities.Car { Brand = "Ford", Model = "Focus", Year = 2023, PricePerDay = 620, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Compact, ImageUrl = "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?w=800", Seats = 5, FuelType = "Benzin", Transmission = "Otomatik" },
            new RentACar.Domain.Entities.Car { Brand = "Toyota", Model = "Corolla", Year = 2023, PricePerDay = 680, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Compact, ImageUrl = "https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb?w=800", Seats = 5, FuelType = "Hibrit", Transmission = "Otomatik" },
            new RentACar.Domain.Entities.Car { Brand = "Nissan", Model = "Qashqai", Year = 2023, PricePerDay = 850, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.SUV, ImageUrl = "https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?w=800", Seats = 5, FuelType = "Dizel", Transmission = "Otomatik" },
            new RentACar.Domain.Entities.Car { Brand = "Peugeot", Model = "3008", Year = 2023, PricePerDay = 880, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.SUV, ImageUrl = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800", Seats = 5, FuelType = "Dizel", Transmission = "Otomatik" },
            new RentACar.Domain.Entities.Car { Brand = "Hyundai", Model = "Tucson", Year = 2023, PricePerDay = 900, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.SUV, ImageUrl = "https://images.unsplash.com/photo-1609521263047-f8f205293f24?w=800", Seats = 5, FuelType = "Hibrit", Transmission = "Otomatik" },
            new RentACar.Domain.Entities.Car { Brand = "BMW", Model = "3 Series", Year = 2024, PricePerDay = 1500, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Premium, ImageUrl = "https://images.unsplash.com/photo-1555215695-3004980ad54e?w=800", Seats = 5, FuelType = "Benzin", Transmission = "Otomatik" },
            new RentACar.Domain.Entities.Car { Brand = "Mercedes", Model = "C-Class", Year = 2024, PricePerDay = 1600, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Premium, ImageUrl = "https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8?w=800", Seats = 5, FuelType = "Dizel", Transmission = "Otomatik" },
            new RentACar.Domain.Entities.Car { Brand = "Audi", Model = "A4", Year = 2024, PricePerDay = 1550, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Premium, ImageUrl = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800", Seats = 5, FuelType = "Dizel", Transmission = "Otomatik" },
            new RentACar.Domain.Entities.Car { Brand = "Volkswagen", Model = "Transporter", Year = 2023, PricePerDay = 950, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Van, ImageUrl = "https://images.unsplash.com/photo-1527786356703-4b100091cd2c?w=800", Seats = 9, FuelType = "Dizel", Transmission = "Manuel" },
            new RentACar.Domain.Entities.Car { Brand = "Ford", Model = "Transit Custom", Year = 2023, PricePerDay = 920, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Van, ImageUrl = "https://images.unsplash.com/photo-1464219789935-c2d9d9aba644?w=800", Seats = 9, FuelType = "Dizel", Transmission = "Manuel" },
            new RentACar.Domain.Entities.Car { Brand = "Mercedes", Model = "Vito", Year = 2023, PricePerDay = 980, Status = RentACar.Domain.Entities.CarStatus.Available, Category = RentACar.Domain.Entities.CarCategory.Van, ImageUrl = "https://images.unsplash.com/photo-1506521781263-d8422e82f27a?w=800", Seats = 8, FuelType = "Dizel", Transmission = "Otomatik" }
        );
        db.SaveChanges();
    }
}

app.Run();
