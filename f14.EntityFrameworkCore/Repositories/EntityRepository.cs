using f14.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace f14.AspNetCore.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Basic repository that provides entities extraction methods.
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    public abstract class EntityRepository<T, TKey> : Repository<T>, IEntityRepository<T, TKey>, IAsyncEntityRepository<T, TKey>
        where T : class, IEntity<TKey>
    {
        /// <summary>
        /// Creates new instance of repository.
        /// </summary>
        /// <param name="table">The queryable collection.</param>
        protected EntityRepository(IQueryable<T> table) : base(table)
        {
        }

        #region IEntityRepository

        ///<inheritdoc cref="IEntityRepository{T, TKey}.Get(TKey)"/>
        public T? Get(TKey id)
        {
            return Get(x => x.Id!.Equals(id));
        }

        #endregion

        #region IAsyncEntityRepository

        ///<inheritdoc cref="IAsyncEntityRepository{T, TKey}.GetAsync(TKey, CancellationToken)"/>
        public async Task<T?> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await GetAsync(x => x.Id!.Equals(id), cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
