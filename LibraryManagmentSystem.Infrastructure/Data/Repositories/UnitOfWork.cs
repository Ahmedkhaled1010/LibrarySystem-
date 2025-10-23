using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.Context;

namespace LibraryManagmentSystem.Infrastructure.Data.Repositories
{
    public class UnitOfWork(LibraryDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var RepoType = typeof(TEntity).Name;
            return CheckRepository<TEntity, TKey>(dbContext, RepoType);
        }

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
    }
}
