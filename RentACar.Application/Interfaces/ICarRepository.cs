using RentACar.Domain.Entities;

namespace RentACar.Application.Interfaces;

public interface ICarRepository : IGenericRepository<Car>
{
    Task<IEnumerable<Car>> GetAvailableCarsAsync();
}
