using LibraryManagmentSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryManagmentSystem.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        IMongoRepository<TEntity> GetMongoRepository<TEntity>() where TEntity : BaseMongoEntity;
        IBorrowRepository borrowRepository { get; }
        ICasheRepository casheRepository { get; }


        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> SaveChangesAsync();
    }
}
