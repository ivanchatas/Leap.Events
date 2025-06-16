using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Persistence.Repositories
{
    public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : class
    {
        protected readonly ISession _session;

        public BaseRepository(ISession session)
        {
            _session = session;
        }

        protected IQueryable<TEntity> PrepareQuery(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public virtual async Task<IQueryable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null
        )
        {
            IQueryable<TEntity> query = _session.Query<TEntity>().AsQueryable();
            query = PrepareQuery(query, predicate, include, orderBy);
            return await Task.FromResult(query);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null
        )
        {

            IQueryable<TEntity> query = _session.Query<TEntity>().AsQueryable();
            query = PrepareQuery(query, predicate, include, orderBy);
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _session.GetAsync<TEntity>(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _session.SaveAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await _session.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                await _session.DeleteAsync(entity);
            }
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            await _session.DeleteAsync(entity);
        }

        public virtual async Task<bool> ExistsAsync(Expression<System.Func<TEntity, bool>> predicate)
        {
            return await _session.Query<TEntity>().AnyAsync(predicate);
        }
    }
}
