namespace f14.Data
{
    /// <summary>
    /// Representation of the basic entity object that usually used by NoSQL databases.
    /// </summary>
    public interface IDocument : IEntity<string>
    {
        /// <summary>
        /// Gets the document type.
        /// </summary>
        string DocumentType { get; }

        /// <summary>
        /// Provides the document type as a string.
        /// </summary>
        /// <typeparam name="T">Type of a target object.</typeparam>
        /// <returns>The string which represent the document type.</returns>
        public static string GetDocumentType<T>() => typeof(T).Name;
    }
}
