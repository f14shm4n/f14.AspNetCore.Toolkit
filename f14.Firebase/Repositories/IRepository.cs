using f14.Data;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace f14.Firebase.Repositories
{
    /// <summary>
    /// Provides Firestore repository that implements basic data access methods.
    /// </summary>
    /// <typeparam name="T">Type of repository objects.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Adds new entity to the collection.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation.</returns>
        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a query that should return a <see cref="List{T}"/>.
        /// </summary>
        /// <param name="offset">Number of items that should be skipped in source ordered collection.</param>
        /// <param name="size">Number of items that should be returned in resulting list.</param>
        /// <param name="beforeExecute">A query builder.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation with specified resulting <see cref="List{T}"/>.</returns>
        Task<List<T>?> ExecuteQueryAsync(int offset, int size, Func<Query, Query> beforeExecute, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the entity with specified id.
        /// </summary>
        /// <param name="id">Entity id to delete.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation.</returns>
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the entity with specified id.
        /// </summary>
        /// <param name="id">Entity id to search.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation with specified resulting entity type.</returns>
        Task<T?> GetAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Patches the entity in a collection with specified id.
        /// </summary>
        /// <param name="id">Entity id to patch.</param>
        /// <param name="patches">A collection of patches.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation.</returns>
        Task PatchAsync(string id, IReadOnlyCollection<PropertyPatch<T>> patches, CancellationToken cancellationToken = default);

        /// <summary>
        /// Patches the specified entity in a collection.
        /// </summary>
        /// <param name="entity">Entity to patch.</param>
        /// <param name="patches">A collection of patches.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation</returns>
        Task PatchAsync(T entity, IReadOnlyCollection<PropertyPatch<T>> patches, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the entity in a collection.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation.</returns>
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    }
}
