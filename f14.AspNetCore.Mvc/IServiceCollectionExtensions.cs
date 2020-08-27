using f14.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
