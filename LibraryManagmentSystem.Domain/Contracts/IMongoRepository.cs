using LibraryManagmentSystem.Domain.Entity;

namespace LibraryManagmentSystem.Domain.Contracts
{
    public interface IMongoRepository<TEntity> where TEntity : BaseMongoEntity
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(Guid id, TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
