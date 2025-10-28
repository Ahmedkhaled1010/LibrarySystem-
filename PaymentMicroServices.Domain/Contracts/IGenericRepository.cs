using PaymentMicroServices.Domain.Models;

namespace PaymentMicroServices.Domain.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(Guid id, TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
