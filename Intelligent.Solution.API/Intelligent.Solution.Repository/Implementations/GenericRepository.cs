using Intelligent.Solution.Domain;
using Intelligent.Solution.Repository.Interfaces;
using Intelligent.Solution.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intelligent.Solution.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly IntelligentContext _context;

        public GenericRepository(IntelligentContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            entity.CreatedOn = DateTime.UtcNow;
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            bool success = false;
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                success = true;
            }
            return success;
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<T?> UpdateAsync(T entity, int id, CancellationToken cancellationToken)
        {
            T? returnEntity = null;
            var existingEntity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
                entity.UpdatedOn = DateTime.UtcNow;
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
                returnEntity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            }
            return returnEntity;
        }
    }
}
