using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Infrastructure.Data.Repositories
{
    public class GenericRepository<TEntity, TKey>(LibraryDbContext libraryDbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity) => await libraryDbContext.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await libraryDbContext.Set<TEntity>().ToListAsync();


        public async Task<TEntity?> GetByIdAsync(TKey id) => await libraryDbContext.Set<TEntity>().FindAsync(id);

        public void Delete(TEntity entity) => libraryDbContext.Set<TEntity>().Remove(entity);



        public void Update(TEntity entity) => libraryDbContext.Set<TEntity>().Update(entity);

    }
}
