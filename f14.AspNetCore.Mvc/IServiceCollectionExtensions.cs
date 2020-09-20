using f14.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace f14.AspNetCore.Mvc
{
    /// <summary>
    /// Provides an extensions methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Addes the <see cref="IViewRenderer"/> as scoped service.
        /// </summary>
        public static IServiceCollection AddViewRenderer(this IServiceCollection services)
        {
            services.AddScoped<IViewRenderer, ViewRenderer>();
            return services;
        }

        /// <summary>
        /// Configures the <see cref="ForwardedHeadersOptions"/> to use forwarded headers: <see cref="ForwardedHeaders.XForwardedFor"/> and <see cref="ForwardedHeaders.XForwardedProto"/>.
        /// </summary>
        public static IServiceCollection ConfigureHttpForwarderHeaders(this IServiceCollection services)
        {
            if (string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_FORWARDEDHEADERS_ENABLED"), "true", StringComparison.OrdinalIgnoreCase))
            {
                services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                    // Only loopback proxies are allowed by default.
                    // Clear that restriction because forwarders are enabled by explicit 
                    // configuration.
                    options.KnownNetworks.Clear();
                    options.KnownProxies.Clear();
                });
            }
            return services;
        }
    }
}
