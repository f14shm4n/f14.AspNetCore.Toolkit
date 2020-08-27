using Microsoft.AspNetCore.DataProtection;
using System;
using System.Text.Json.Serialization;

namespace f14.AspNetCore.DataProtection
{
    /// <summary>
    /// Represents the json options for DataProtection system.
    /// </summary>
    public class ExtendedDataProtectionOptions : DataProtectionOptions
    {
        /// <summary>
        /// Dethemines the storage type.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StorageType StorageType { get; set; }

        /// <summary>
        /// Determines the keys lifetime.
        /// </summary>
        public TimeSpan? KeysLifetime { get; set; }

        /// <summary>
        /// Provides the Azure Blob Storage DP settings.
        /// </summary>
        public AzureStorageSettings? AzureBlobStorage { get; set; }

        /// <summary>
        /// Provides the file system DP settings.
        /// </summary>
        public FileSystemSettings? FileSystem { get; set; }

        /// <summary>
        /// Gets the <see cref="IStorageConfigurator"/> based on the <see cref="StorageType"/> property value.
        /// </summary>
        /// <returns>The <see cref="IStorageConfigurator"/> instance of null.</returns>
        public IStorageConfigurator? GetConfigurator()
        {
            return StorageType switch
            {
                StorageType.FileSystem => FileSystem,
                StorageType.AzureBlobStorage => AzureBlobStorage,
                _ => null
            };
        }
    }
}
