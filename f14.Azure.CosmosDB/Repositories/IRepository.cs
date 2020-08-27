using f14.Data;
using Microsoft.Azure.Cosmos;
using System.Threading;
using System.Threading.Tasks;

namespace f14.Azure.CosmosDB.Repositories
{
    /// <summary>
    /// Represents the repository with specific type.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public interface IRepository<T> where T : class, IDocument
    {
        /// <summary>
        /// Adds an entity to the database.
        /// </summary>
        /// <param name="entity">Entity to be add.</param>
        /// <param name="partitionKey">Entity partition key.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation with entity resulting object.</returns>
        Task<T> AddAsync(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="entity">Entity to be delete.</param>
        /// <param name="partitionKey">Entity partition key.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation with entity resulting object.</returns>
        Task DeleteAsync(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an entity from the database.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <param name="partitionKey">Entity partition key.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation with entity resulting object.</returns>
        Task<T> GetByIdAsync(string id, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an entity in the database.
        /// </summary>
        /// <param name="entity">Entity to be update.</param>
        /// <param name="partitionKey">Entity partition key.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation with entity resulting object.</returns>
        Task<T> UpdateAsync(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default);
    }
}
