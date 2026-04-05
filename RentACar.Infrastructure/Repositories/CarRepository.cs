using Microsoft.EntityFrameworkCore;
using RentACar.Application.Interfaces;
using RentACar.Domain.Entities;
using RentACar.Infrastructure.Data;

namespace RentACar.Infrastructure.Repositories;

public class CarRepository : GenericRepository<Car>, ICarRepository
{
    public CarRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Car>> GetAvailableCarsAsync() =>
        await _dbSet.Where(c => c.Status == CarStatus.Available).ToListAsync();
}
