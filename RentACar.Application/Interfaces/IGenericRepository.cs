namespace RentACar.Application.Interfaces;

/// <summary>
/// Generic repository pattern - tüm entity'ler için temel CRUD operasyonları
/// </summary>
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
