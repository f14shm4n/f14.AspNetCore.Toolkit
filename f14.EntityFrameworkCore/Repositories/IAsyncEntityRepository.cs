using f14.Data;
using System.Threading;
using System.Threading.Tasks;

namespace f14.AspNetCore.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// The <see cref="IEntity{TKey}"/> async repository that provides extract methods.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    public interface IAsyncEntityRepository<T, TKey> : IAsyncRepository<T>
        where T : class, IEntity<TKey>
    {
        /// <summary>
        /// Searches the single object using specified selector.
        /// </summary>
        /// <param name="id">The desired entity key to search for.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>The async task which returns a single entity or null.</returns>
        Task<T?> GetAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
