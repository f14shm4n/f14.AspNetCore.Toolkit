using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace f14.AspNetCore.Mvc.Razor
{
    /// <summary>
    /// The service which provides a view rendering features to a string representation.
    /// <para>
    ///     Sources: 'https://ppolyzos.com/2016/09/09/asp-net-core-render-view-to-string/'.
    /// </para>
    /// </summary>
    public sealed class ViewRenderer : IViewRenderer
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;

        /// <summary>
        /// Creates new instance of the view renderer.
        /// </summary>
        /// <param name="razorViewEngine">Razor view engine.</param>
        /// <param name="tempDataProvider">Temporary data provider.</param>
        public ViewRenderer(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
        }

        ///<inheritdoc/>
        public async Task<string> RenderAsync(HttpContext httpContext, string viewName, object model, IDictionary<string, object> viewData, bool fromCustomPath = false)
        {
            var actionContext = new ActionContext(httpContext, httpContext.GetRouteData(), new ActionDescriptor());
            var viewResult = fromCustomPath
                ? _razorViewEngine.GetView(null, viewName, false)
                : _razorViewEngine.FindView(actionContext, viewName, false);

            using (var writer = new StringWriter())
            {
                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewResult.ViewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                if (viewData != null && viewData.Count > 0)
                {
                    foreach (var kv in viewData)
                    {
                        viewDictionary[kv.Key] = kv.Value;
                    }
                }

                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, new TempDataDictionary(actionContext.HttpContext, _tempDataProvider), writer, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext).ConfigureAwait(false);

                return writer.ToString();
            }
        }
    }
}
