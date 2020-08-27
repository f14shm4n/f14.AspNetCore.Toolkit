using Microsoft.AspNetCore.DataProtection;
using System;

namespace f14.AspNetCore.DataProtection
{
    /// <summary>
    /// Provides the data protection settings to store keys in file system.
    /// </summary>
    public sealed class FileSystemSettings : IStorageConfigurator
    {
        /// <summary>
        /// The directory path where the application will stores the data protection keys.
        /// </summary>
        public string DirectoryPath { get; set; } = string.Empty;

        ///<inheritdoc/>
        public void Configure(IDataProtectionBuilder builder)
        {
            if (string.IsNullOrWhiteSpace(DirectoryPath))
            {
                throw new InvalidOperationException("Missing the directory path for DataProtection.");
            }

            builder.PersistKeysToFileSystem(new System.IO.DirectoryInfo(DirectoryPath));
        }
    }
}
