using RentACar.Domain.Entities;

namespace RentACar.Application.Interfaces;

/// <summary>
/// Araç repository interface'i - CRUD operasyonları
/// </summary>
public interface ICarRepository : IGenericRepository<Car>
{
    Task<IEnumerable<Car>> GetAvailableCarsAsync();
}
