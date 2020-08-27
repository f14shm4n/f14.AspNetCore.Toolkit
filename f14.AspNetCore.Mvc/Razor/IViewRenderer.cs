using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace f14.AspNetCore.Mvc.Razor
{
    /// <summary>
    /// Service that provides a view rendering features to a string representation.
    /// </summary>
    public interface IViewRenderer
    {
        /// <summary>
        /// Renders the Razor view to a string;
        /// </summary>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <param name="viewName">Desired view name.</param>
        /// <param name="model">Model that will be passed to a view.</param>
        /// <param name="viewData">View data dictionary.</param>
        /// <param name="fromCustomPath">Determines whether the service should search views in non default locations.</param>
        /// <returns>An async task which represents the rendering operation, the task result is a string representation of the view.</returns>
        Task<string> RenderAsync(HttpContext httpContext, string viewName, object model, IDictionary<string, object> viewData, bool fromCustomPath = false);
    }
}
