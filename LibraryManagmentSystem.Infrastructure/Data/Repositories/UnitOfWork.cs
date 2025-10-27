using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.Context;
using LibraryManagmentSystem.Infrastructure.Data.MongoContext;

namespace LibraryManagmentSystem.Infrastructure.Data.Repositories
{
    public class UnitOfWork(LibraryDbContext dbContext, MongoDb mongoDb) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repositories = [];
        private readonly Dictionary<string, object> mongoRepositories = [];

        public IMongoRepository<TEntity> GetMongoRepository<TEntity>() where TEntity : BaseMongoEntity
        {
            var RepoType = typeof(TEntity).Name;
            return CheckMongoRepository<TEntity>(mongoDb, RepoType);
        }



        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var RepoType = typeof(TEntity).Name;
            return CheckRepository<TEntity, TKey>(dbContext, RepoType);
        }
        public IBorrowRepository borrowRepository => new BorrowRepository(dbContext);

        public async Task<int> SaveChangesAsync() => await dbContext.SaveChangesAsync();

        private IGenericRepository<TEntity, TKey> CheckRepository<TEntity, TKey>(LibraryDbContext dbContext, string RepoType) where TEntity : BaseEntity<TKey>
        {
            if (repositories.ContainsKey(RepoType))
            {
                return (IGenericRepository<TEntity, TKey>)repositories[RepoType];
            }
            else
            {
                var Repo = new GenericRepository<TEntity, TKey>(dbContext);
                repositories.Add(RepoType, Repo);
                return Repo;
            }
        }
        private IMongoRepository<TEntity> CheckMongoRepository<TEntity>(MongoDb mongoDb, string RepoType) where TEntity : BaseMongoEntity
        {
            if (mongoRepositories.ContainsKey(RepoType))
            {
                return (IMongoRepository<TEntity>)mongoRepositories[RepoType];
            }
            else
            {
                var Repo = new MongoRepository<TEntity>(mongoDb);
                mongoRepositories.Add(RepoType, Repo);
                return Repo;
            }
        }
    }
}
