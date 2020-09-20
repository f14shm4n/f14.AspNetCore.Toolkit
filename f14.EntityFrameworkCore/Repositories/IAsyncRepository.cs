using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace f14.AspNetCore.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// The repository which provides async methods to extract objects.
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    public interface IAsyncRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities in the related table.
        /// </summary>
        /// <returns>The async task which returns the <see cref="IEnumerable{T}"/> of entities or null.</returns>
        Task<IEnumerable<T>?> GetAllAsync();

        /// <summary>
        /// Searches for an entities using the specified filter.
        /// </summary>
        /// <param name="filter">The entity filter.</param>
        /// <param name="skip">The number of an entities that need to skip in filterd collection.</param>
        /// <param name="take">The number of an entities that need to take from the filtered collection.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>The async task which returns the <see cref="IEnumerable{T}"/> of entities or null.</returns>
        Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> filter, int skip, int take, CancellationToken cancellationToken = default);

        /// <summary>
        /// Searches the single object using specified selector.
        /// </summary>
        /// <param name="selector">The entity selector.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>The async task which returns a single entity or null.</returns>
        Task<T?> GetAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the number of entities in the collection.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>The async task which returns the number of objects.</returns>
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the number of entities in the collection using specified filter.
        /// </summary>
        /// <param name="filter">The entity filter.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>The async task which returns the number of objects.</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    }
}
