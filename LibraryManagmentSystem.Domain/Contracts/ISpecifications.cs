using LibraryManagmentSystem.Domain.Entity;
using System.Linq.Expressions;

namespace LibraryManagmentSystem.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        public Expression<Func<TEntity, object>>? OrderBy { get; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; }

        public Expression<Func<TEntity, object>>? GroupBy { get; }
        public Expression<Func<TEntity, object>> Selector { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPaginated { get; set; }
    }
}
