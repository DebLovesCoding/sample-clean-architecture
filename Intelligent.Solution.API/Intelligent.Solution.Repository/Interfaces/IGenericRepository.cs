using Intelligent.Solution.Domain.Entities;

namespace Intelligent.Solution.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<T?> UpdateAsync(T entity, int id, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
