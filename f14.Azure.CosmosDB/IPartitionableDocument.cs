using f14.Data;

namespace f14.Azure.CosmosDB
{
    /// <summary>
    /// Represents the document that support the partition key.
    /// </summary>
    public interface IPartitionableDocument : IDocument
    {
        /// <summary>
        /// The resourse partition key.
        /// </summary>
        string PartitionKey { get; set; }
    }
}
