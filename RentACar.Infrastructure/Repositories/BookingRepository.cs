using Microsoft.EntityFrameworkCore;
using RentACar.Application.Interfaces;
using RentACar.Domain.Entities;
using RentACar.Infrastructure.Data;

namespace RentACar.Infrastructure.Repositories;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId) =>
        await _dbSet.Include(b => b.Car)
                    .Where(b => b.UserId == userId)
                    .ToListAsync();

    public async Task<IEnumerable<Booking>> GetBookingsWithCarAsync() =>
        await _dbSet.Include(b => b.Car).ToListAsync();
}
