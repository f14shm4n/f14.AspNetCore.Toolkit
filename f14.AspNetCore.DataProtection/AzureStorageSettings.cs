using Microsoft.AspNetCore.DataProtection;
using System;

namespace f14.AspNetCore.DataProtection
{
    /// <summary>
    /// Provides the data protection settings to store keys in Azure Storage.
    /// </summary>
    public sealed class AzureStorageSettings : IStorageConfigurator
    {
        /// <summary>
        /// The url for the blob where the application will stores the data protection keys.
        /// </summary>
        public string BlobUrl { get; set; } = string.Empty;

        ///<inheritdoc/>
        public void Configure(IDataProtectionBuilder builder)
        {
            if (string.IsNullOrWhiteSpace(BlobUrl))
            {
                throw new InvalidOperationException("Missing the Azure Storage Blob url for DataProtection.");
            }

            builder.PersistKeysToAzureBlobStorage(new Uri(BlobUrl));
        }
    }
}
