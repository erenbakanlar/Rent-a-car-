using RentACar.Domain.Entities;

namespace RentACar.Application.Interfaces;

public interface IBookingRepository : IGenericRepository<Booking>
{
    Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId);
    Task<IEnumerable<Booking>> GetBookingsWithCarAsync();
}
