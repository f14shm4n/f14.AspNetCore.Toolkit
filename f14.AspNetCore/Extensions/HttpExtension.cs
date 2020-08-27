using f14.IO;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// Provides an extensions methods for Http classes.
    /// </summary>
    public static class HttpExtension
    {
        /// <summary>
        /// Reads the <see cref="HttpRequest.Body"/> as string.
        /// </summary>
        /// <param name="request">HTTP request.</param>
        /// <returns>The body content as string.</returns>
        public static string ReadBody(this HttpRequest request)
        {
            return FileIO.ReadToEnd(request.Body, Encoding.UTF8, true, 1024, true);
        }

        /// <summary>
        /// Asynchronously readsthe <see cref="HttpRequest.Body"/> as string.
        /// </summary>
        /// <param name="request">HTTP request.</param>
        /// <returns>The <see cref="Task{T}"/> that can be awaited, the resulting object is the string read from request body.</returns>
        public static async Task<string> ReadBodyAsync(this HttpRequest request)
        {
            return await FileIO.ReadToEndAsync(request.Body, Encoding.UTF8, true, 1024, true).ConfigureAwait(false);
        }
    }
}
