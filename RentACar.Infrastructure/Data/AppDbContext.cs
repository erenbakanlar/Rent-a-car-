using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Domain.Entities;

namespace RentACar.Infrastructure.Data;

/// <summary>
/// Uygulama veritabanı context'i - Identity ile entegre
/// </summary>
public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Car konfigürasyonu
        builder.Entity<Car>(entity =>
        {
            entity.Property(c => c.PricePerDay).HasColumnType("decimal(18,2)");
            entity.Property(c => c.Brand).HasMaxLength(100).IsRequired();
            entity.Property(c => c.Model).HasMaxLength(100).IsRequired();
        });

        // Booking konfigürasyonu
        builder.Entity<Booking>(entity =>
        {
            entity.Property(b => b.TotalPrice).HasColumnType("decimal(18,2)");
            entity.HasOne(b => b.Car)
                  .WithMany(c => c.Bookings)
                  .HasForeignKey(b => b.CarId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Seed Data - Lüksten düşüğe araçlar
        builder.Entity<Car>().HasData(
            // ── Premium ──────────────────────────────────────────
            new Car { Id = 1,  Brand = "BMW",        Model = "7 Series",     Year = 2024, PricePerDay = 4500, Category = CarCategory.Premium,  Status = CarStatus.Available, Seats = 5, FuelType = "Benzin",   Transmission = "Otomatik", ImageUrl = "https://images.unsplash.com/photo-1555215695-3004980ad54e?w=600&q=80" },
            new Car { Id = 2,  Brand = "Mercedes",   Model = "S-Class",      Year = 2024, PricePerDay = 5200, Category = CarCategory.Premium,  Status = CarStatus.Available, Seats = 5, FuelType = "Hibrit",   Transmission = "Otomatik", ImageUrl = "https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8?w=600&q=80" },
            new Car { Id = 3,  Brand = "Audi",       Model = "A8",           Year = 2023, PricePerDay = 4200, Category = CarCategory.Premium,  Status = CarStatus.Available, Seats = 5, FuelType = "Dizel",    Transmission = "Otomatik", ImageUrl = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=600&q=80" },
            new Car { Id = 4,  Brand = "Porsche",    Model = "Cayenne",      Year = 2024, PricePerDay = 6000, Category = CarCategory.Premium,  Status = CarStatus.Available, Seats = 5, FuelType = "Hibrit",   Transmission = "Otomatik", ImageUrl = "https://images.unsplash.com/photo-1503376780353-7e6692767b70?w=600&q=80" },
            // ── SUV ──────────────────────────────────────────────
            new Car { Id = 5,  Brand = "Toyota",     Model = "Land Cruiser", Year = 2023, PricePerDay = 2800, Category = CarCategory.SUV,      Status = CarStatus.Available, Seats = 7, FuelType = "Dizel",    Transmission = "Otomatik", ImageUrl = "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?w=600&q=80" },
            new Car { Id = 6,  Brand = "Nissan",     Model = "Qashqai",      Year = 2023, PricePerDay = 1600, Category = CarCategory.SUV,      Status = CarStatus.Available, Seats = 5, FuelType = "Benzin",   Transmission = "Otomatik", ImageUrl = "https://images.unsplash.com/photo-1609521263047-f8f205293f24?w=600&q=80" },
            new Car { Id = 7,  Brand = "Hyundai",    Model = "Tucson",       Year = 2024, PricePerDay = 1750, Category = CarCategory.SUV,      Status = CarStatus.Available, Seats = 5, FuelType = "Hibrit",   Transmission = "Otomatik", ImageUrl = "https://images.unsplash.com/photo-1552519507-da3b142c6e3d?w=600&q=80" },
            new Car { Id = 8,  Brand = "Peugeot",    Model = "3008",         Year = 2023, PricePerDay = 1500, Category = CarCategory.SUV,      Status = CarStatus.Available, Seats = 5, FuelType = "Dizel",    Transmission = "Otomatik", ImageUrl = "https://images.unsplash.com/photo-1541899481282-d53bffe3c35d?w=600&q=80" },
            // ── Van ──────────────────────────────────────────────
            new Car { Id = 9,  Brand = "Ford",       Model = "Tourneo",      Year = 2023, PricePerDay = 2200, Category = CarCategory.Van,      Status = CarStatus.Available, Seats = 9, FuelType = "Dizel",    Transmission = "Manuel",   ImageUrl = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=600&q=80" },
            new Car { Id = 10, Brand = "Volkswagen", Model = "Caravelle",    Year = 2022, PricePerDay = 2400, Category = CarCategory.Van,      Status = CarStatus.Available, Seats = 9, FuelType = "Dizel",    Transmission = "Otomatik", ImageUrl = "https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?w=600&q=80" },
            // ── Compact ──────────────────────────────────────────
            new Car { Id = 11, Brand = "Volkswagen", Model = "Golf",         Year = 2023, PricePerDay = 1100, Category = CarCategory.Compact,  Status = CarStatus.Available, Seats = 5, FuelType = "Benzin",   Transmission = "Manuel",   ImageUrl = "https://images.unsplash.com/photo-1471444928139-48c5bf5173f8?w=600&q=80" },
            new Car { Id = 12, Brand = "Toyota",     Model = "Corolla",      Year = 2024, PricePerDay = 1050, Category = CarCategory.Compact,  Status = CarStatus.Available, Seats = 5, FuelType = "Hibrit",   Transmission = "Otomatik", ImageUrl = "https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb?w=600&q=80" },
            new Car { Id = 13, Brand = "Renault",    Model = "Megane",       Year = 2023, PricePerDay = 950,  Category = CarCategory.Compact,  Status = CarStatus.Available, Seats = 5, FuelType = "Dizel",    Transmission = "Manuel",   ImageUrl = "https://images.unsplash.com/photo-1580273916550-e323be2ae537?w=600&q=80" },
            // ── Economy ──────────────────────────────────────────
            new Car { Id = 14, Brand = "Renault",    Model = "Clio",         Year = 2023, PricePerDay = 750,  Category = CarCategory.Economy,  Status = CarStatus.Available, Seats = 5, FuelType = "Benzin",   Transmission = "Manuel",   ImageUrl = "https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?w=600&q=80" },
            new Car { Id = 15, Brand = "Fiat",       Model = "Egea",         Year = 2024, PricePerDay = 700,  Category = CarCategory.Economy,  Status = CarStatus.Available, Seats = 5, FuelType = "Benzin",   Transmission = "Manuel",   ImageUrl = "https://images.unsplash.com/photo-1590362891991-f776e747a588?w=600&q=80" },
            new Car { Id = 16, Brand = "Hyundai",    Model = "i20",          Year = 2023, PricePerDay = 720,  Category = CarCategory.Economy,  Status = CarStatus.Available, Seats = 5, FuelType = "Benzin",   Transmission = "Manuel",   ImageUrl = "https://images.unsplash.com/photo-1494976388531-d1058494cdd8?w=600&q=80" },
            new Car { Id = 17, Brand = "Opel",       Model = "Corsa",        Year = 2023, PricePerDay = 680,  Category = CarCategory.Economy,  Status = CarStatus.Available, Seats = 5, FuelType = "Benzin",   Transmission = "Manuel",   ImageUrl = "https://images.unsplash.com/photo-1502877338535-766e1452684a?w=600&q=80" },
            new Car { Id = 18, Brand = "Peugeot",    Model = "208",          Year = 2024, PricePerDay = 710,  Category = CarCategory.Economy,  Status = CarStatus.Available, Seats = 5, FuelType = "Elektrik", Transmission = "Otomatik", ImageUrl = "https://images.unsplash.com/photo-1568605117036-5fe5e7bab0b7?w=600&q=80" }
        );
    }
}
