using f14.Data;
using Microsoft.Azure.Cosmos;
using System.Threading;
using System.Threading.Tasks;

namespace f14.Azure.CosmosDB.Repositories
{
    ///<inheritdoc cref="IRepository{T}"/>
    public abstract class Repository<T> : IRepository<T> where T : class, IDocument
    {
        /// <summary>
        /// Creates new repository instance.
        /// </summary>
        /// <param name="cosmosAccessor">The <see cref="CosmosClient"/> accessor.</param>
        /// <param name="databaseName">The working database name.</param>
        /// <param name="collectionName">The working container name.</param>
        protected Repository(ICosmosClientAccessor cosmosAccessor, string databaseName, string collectionName)
        {
            CosmosAccessor = cosmosAccessor;
            Container = CosmosAccessor.Client.GetContainer(databaseName, collectionName);
        }

        #region Properties

        /// <summary>
        /// The <see cref="CosmosClient"/> accessor.
        /// </summary>
        protected ICosmosClientAccessor CosmosAccessor { get; }
        /// <summary>
        /// The working container.
        /// </summary>
        protected Container Container { get; }

        #endregion

        #region IRepository

        ///<inheritdoc/>
        public virtual async Task<T> AddAsync(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default)
        {
            return await Container.CreateItemAsync(entity, partitionKey, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public virtual async Task DeleteAsync(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default)
        {
            if (!partitionKey.HasValue)
            {
                partitionKey = PartitionKey.None;
            }

            await Container.DeleteItemAsync<T>(entity.Id, partitionKey.Value, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public virtual async Task<T> GetByIdAsync(string id, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default)
        {
            if (!partitionKey.HasValue)
            {
                partitionKey = PartitionKey.None;
            }

            return await Container.ReadItemAsync<T>(id, partitionKey.Value, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public virtual async Task<T> UpdateAsync(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default)
        {
            return await Container.ReplaceItemAsync(entity, entity.Id, partitionKey, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
