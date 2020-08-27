using f14.Data;
using Newtonsoft.Json;

namespace f14.Azure.CosmosDB
{
    /// <summary>
    /// Represents the basic CosmosDb object.
    /// </summary>
    public class Document : IDocument
    {
        /// <summary>
        /// The resourse id.
        /// </summary>
        [JsonProperty("id")]
        public virtual string Id { get; set; } = string.Empty;
        /// <summary>
        /// The type of the current object. Can be used as a filter.
        /// </summary>
        [JsonProperty("documentType")]
        public string DocumentType => GetType().Name;
    }
}
