using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace f14.AspNetCore.DataProtection
{
    /// <summary>
    /// Provides an extensions methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the DataProtection uses a specified configuration.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="sectionName">The configuration section name, where stored the data protection settings.</param>
        /// <param name="configuration">The application configuration. Usually it is a appsettings.json representation.</param>
        public static void AddDataProtection(this IServiceCollection services, string sectionName, IConfiguration configuration)
        {
            var opts = configuration.GetSection(sectionName).Get<ExtendedDataProtectionOptions>();            
            if (opts.StorageType == StorageType.None)
            {
                return;
            }

            services.AddDataProtection(opts);
        }

        /// <summary>
        /// Configures the DataProtection uses a specified <see cref="ExtendedDataProtectionOptions"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="opts">The data protection options.</param>
        public static void AddDataProtection(this IServiceCollection services, ExtendedDataProtectionOptions opts)
        {
            if (opts.StorageType == StorageType.None)
            {
                return;
            }

            var builder = services.AddDataProtection();            
            var storageConfigurator = opts.GetConfigurator();
            
            // And configure the storage
            storageConfigurator?.Configure(builder);

            // Sets the keys lifetime
            if (opts.KeysLifetime.HasValue)
            {
                builder.SetDefaultKeyLifetime(opts.KeysLifetime.Value);
            }

            // Sets the app name
            if (!string.IsNullOrWhiteSpace(opts.ApplicationDiscriminator))
            {
                builder.SetApplicationName(opts.ApplicationDiscriminator);
            }
        }
    }
}
