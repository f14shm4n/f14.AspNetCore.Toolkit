using f14.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace f14.Firebase.Repositories
{
    /// <summary>
    /// Provides Firestore repository that implements basic data access methods, where entities stored with key as entity type.
    /// </summary>
    public interface IDocumentTypeRepository
    {
        /// <summary>
        /// Adds new entity to the collection.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>     
        /// <returns>An async task that represent the execution operation.</returns>
        Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, IDocument;

        /// <summary>
        /// Deletes the entity with specified <typeparamref name="T"/> as the entity key.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>        
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation.</returns>
        Task DeleteAsync<T>(CancellationToken cancellationToken = default) where T : class, IDocument;

        /// <summary>
        /// Gets an entity with specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>        
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation with specified resulting entity type.</returns>
        Task<T?> GetAsync<T>(CancellationToken cancellationToken = default) where T : class, IDocument;

        /// <summary>
        /// Patches an entity with specified type <typeparamref name="T"/> as the entity key.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="patches">A collection of patches.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation.</returns>
        Task PatchAsync<T>(IReadOnlyCollection<PropertyPatch<T>> patches, CancellationToken cancellationToken = default) where T : class, IDocument;

        /// <summary>
        /// Updates the entity in a collection.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">Entity to update.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation.</returns>
        Task UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, IDocument;
    }
}
