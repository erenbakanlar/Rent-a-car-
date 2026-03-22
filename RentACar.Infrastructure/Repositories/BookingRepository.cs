using Microsoft.EntityFrameworkCore;
using RentACar.Application.Interfaces;
using RentACar.Domain.Entities;
using RentACar.Infrastructure.Data;

namespace RentACar.Infrastructure.Repositories;

/// <summary>
/// Rezervasyon repository implementasyonu
/// </summary>
public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext context) : base(context) { }

    /// <summary>
    /// Belirli bir kullanıcının rezervasyonlarını getirir
    /// </summary>
    public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId) =>
        await _dbSet.Include(b => b.Car)
                    .Where(b => b.UserId == userId)
                    .ToListAsync();

    /// <summary>
    /// Tüm rezervasyonları araç bilgisiyle birlikte getirir
    /// </summary>
    public async Task<IEnumerable<Booking>> GetBookingsWithCarAsync() =>
        await _dbSet.Include(b => b.Car).ToListAsync();
}
