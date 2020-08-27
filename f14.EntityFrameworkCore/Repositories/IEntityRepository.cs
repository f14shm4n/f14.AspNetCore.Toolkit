using f14.Data;

namespace f14.AspNetCore.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// The <see cref="IEntity{TKey}"/> repository that provides extract methods.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    public interface IEntityRepository<T, TKey> : IRepository<T>
        where T : class, IEntity<TKey>
    {
        /// <summary>
        /// Searches the single object using specified entity key.
        /// </summary>
        /// <param name="id">The desired entity key to search for.</param>
        /// <returns>The entity or null.</returns>
        T? Get(TKey id);
    }
}
