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
    }
}
