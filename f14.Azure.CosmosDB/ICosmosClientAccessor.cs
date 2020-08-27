using Microsoft.Azure.Cosmos;

namespace f14.Azure.CosmosDB
{
    /// <summary>
    /// Provides access to the <see cref="CosmosClient"/>. The main purpose of this interface is using as the Dependency Injection.
    /// </summary>
    public interface ICosmosClientAccessor
    {
        /// <summary>
        /// Returns the <see cref="CosmosClient"/> instance.
        /// </summary>
        CosmosClient Client { get; }
    }
}
