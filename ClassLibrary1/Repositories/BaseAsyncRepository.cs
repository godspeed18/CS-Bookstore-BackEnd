using ITPLibrary.Application.Contracts.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Infrastructure.Persistance.Repositories
{
    public class BaseAsyncRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly ITPLibraryDbContext _db;

        public BaseAsyncRepository(ITPLibraryDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
