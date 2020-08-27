using f14.Data;
using Google.Cloud.Firestore;
using System.Text.Json.Serialization;

namespace f14.Firebase
{
    /// <summary>
    /// An implementation of the <see cref="IDocument"/> for Firestore database purposes.
    /// </summary>
    public class Document : IDocument
    {
        /// <summary>
        /// The document key.
        /// </summary>
        [FirestoreDocumentId]
        [JsonPropertyName("id")]
        public virtual string Id { get; set; } = string.Empty;

        /// <summary>
        /// The document type.
        /// </summary>
        [FirestoreProperty("documentType")]
        [JsonPropertyName("documentType")]
        public string DocumentType => GetType().Name;
    }
}
