using Microsoft.EntityFrameworkCore;
using RentACar.Application.Interfaces;
using RentACar.Domain.Entities;
using RentACar.Infrastructure.Data;

namespace RentACar.Infrastructure.Repositories;

/// <summary>
/// Araç repository implementasyonu
/// </summary>
public class CarRepository : GenericRepository<Car>, ICarRepository
{
    public CarRepository(AppDbContext context) : base(context) { }

    /// <summary>
    /// Sadece müsait araçları getirir
    /// </summary>
    public async Task<IEnumerable<Car>> GetAvailableCarsAsync() =>
        await _dbSet.Where(c => c.Status == CarStatus.Available).ToListAsync();
}
