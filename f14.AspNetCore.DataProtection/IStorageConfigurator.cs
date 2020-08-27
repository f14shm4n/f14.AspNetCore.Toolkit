using Microsoft.AspNetCore.DataProtection;

namespace f14.AspNetCore.DataProtection
{
    /// <summary>
    /// Represents the DataProtection key storage configurator.
    /// </summary>
    public interface IStorageConfigurator
    {
        /// <summary>
        /// Configures the storage for keeping DP keys for specified <see cref="IDataProtectionBuilder"/>.
        /// </summary>
        /// <param name="builder">The data protection builder.</param>
        void Configure(IDataProtectionBuilder builder);
    }
}
