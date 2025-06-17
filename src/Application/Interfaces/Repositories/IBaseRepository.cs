using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity, TId> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null
        );

        Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null
        );

        Task<TEntity> GetByIdAsync(TId id);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TId id);

        Task DeleteAsync(TEntity entity);

        Task<bool> ExistsAsync(Expression<System.Func<TEntity, bool>> predicate);

        Task<IQueryable<TEntity>> GetBySqlQuery(Type type, string sql);
    }
}
