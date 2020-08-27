using f14.Data;
using Microsoft.Azure.Cosmos;
using System.Threading;
using System.Threading.Tasks;

namespace f14.Azure.CosmosDB.Repositories
{
    /// <summary>
    /// Provides Firestore repository that implements basic data access methods, where entities stored with key as entity type.
    /// </summary>
    public interface IDocumentTypeRepository
    {
        /// <summary>
        /// Adds an entity to the database.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entity">Entity to be add.</param>
        /// <param name="partitionKey">Entity partition key.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation with entity resulting object.</returns>
        Task<T> AddAsync<T>(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default) where T : class, IDocument;

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entity">Entity to be delete.</param>
        /// <param name="partitionKey">Entity partition key.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation with entity resulting object.</returns>
        Task DeleteAsync<T>(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default) where T : class, IDocument;

        /// <summary>
        /// Gets an entity from the database.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="partitionKey">Entity partition key.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation with entity resulting object.</returns>
        Task<T> GetAsync<T>(PartitionKey? partitionKey = null, CancellationToken cancellationToken = default) where T : class, IDocument;

        /// <summary>
        /// Updates an entity in the database.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entity">Entity to be update.</param>
        /// <param name="partitionKey">Entity partition key.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation with entity resulting object.</returns>
        Task<T> UpdateAsync<T>(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default) where T : class, IDocument;
    }
}
