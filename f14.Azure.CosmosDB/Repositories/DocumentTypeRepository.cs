using f14.Data;
using Microsoft.Azure.Cosmos;
using System.Threading;
using System.Threading.Tasks;

namespace f14.Azure.CosmosDB.Repositories
{
    ///<inheritdoc cref="IDocumentTypeRepository"/>
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        /// <summary>
        /// Creates new repository instance.
        /// </summary>
        /// <param name="cosmosAccessor">The <see cref="CosmosClient"/> accessor.</param>
        /// <param name="databaseName">The working database name.</param>
        /// <param name="collectionName">The working container name.</param>
        protected DocumentTypeRepository(ICosmosClientAccessor cosmosAccessor, string databaseName, string collectionName)
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

        #region IDocumentTypeRepository

        ///<inheritdoc/>
        public virtual async Task<T> AddAsync<T>(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default) where T : class, IDocument
        {
            entity.Id = entity.DocumentType;
            return await Container.CreateItemAsync(entity, partitionKey, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public virtual async Task DeleteAsync<T>(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default) where T : class, IDocument
        {
            if (!partitionKey.HasValue)
            {
                partitionKey = PartitionKey.None;
            }

            await Container.DeleteItemAsync<T>(entity.DocumentType, partitionKey.Value, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public virtual async Task<T> GetAsync<T>(PartitionKey? partitionKey = null, CancellationToken cancellationToken = default) where T : class, IDocument
        {
            if (!partitionKey.HasValue)
            {
                partitionKey = PartitionKey.None;
            }

            return await Container.ReadItemAsync<T>(IDocument.GetDocumentType<T>(), partitionKey.Value, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public virtual async Task<T> UpdateAsync<T>(T entity, PartitionKey? partitionKey = null, CancellationToken cancellationToken = default) where T : class, IDocument
        {
            return await Container.ReplaceItemAsync(entity, entity.DocumentType, partitionKey, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
