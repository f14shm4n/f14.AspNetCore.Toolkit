namespace f14.AspNetCore.DataProtection
{
    /// <summary>
    /// Defines the supported data protection storages.
    /// </summary>
    public enum StorageType
    {
        /// <summary>
        /// Not specified.
        /// </summary>
        None,
        /// <summary>
        /// Stores keys in the file system.
        /// </summary>
        FileSystem,
        /// <summary>
        /// Stores keys in Azure Blob Storage.
        /// </summary>
        AzureBlobStorage
    }
}
