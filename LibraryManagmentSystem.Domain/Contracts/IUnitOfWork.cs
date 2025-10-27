using LibraryManagmentSystem.Domain.Entity;

namespace LibraryManagmentSystem.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        IMongoRepository<TEntity> GetMongoRepository<TEntity>() where TEntity : BaseMongoEntity;
        IBorrowRepository borrowRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
