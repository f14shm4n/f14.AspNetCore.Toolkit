using Microsoft.AspNetCore.Html;
using System.IO;
using System.Text.Encodings.Web;

namespace f14.AspNetCore.Extensions
{
    /// <summary>
    /// Extends the <see cref="Microsoft.AspNetCore.Html"/>.
    /// </summary>
    public static class HtmlExtension
    {
        /// <summary>
        /// Create <see cref="IHtmlContent"/> from given string.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>The <see cref="IHtmlContent"/> as result.</returns>
        public static IHtmlContent AsHtml(this string source) => new HtmlString(source);

        /// <summary>
        /// Writes the current html content to the string instance with <see cref="HtmlEncoder.Default"/>.
        /// </summary>
        /// <param name="htmlContent">The html content to write to the string.</param>
        /// <returns>The html as string.</returns>
        public static string AsString(this IHtmlContent htmlContent) => htmlContent.AsString(HtmlEncoder.Default);

        /// <summary>
        /// Writes the current html content to the string instance.
        /// </summary>
        /// <param name="htmlContent">The html content to write to the string.</param>
        /// <param name="htmlEncoder">The html encoder.</param>
        /// <returns>The html as string.</returns>
        public static string AsString(this IHtmlContent htmlContent, HtmlEncoder htmlEncoder)
        {
            var writer = new StringWriter();
            htmlContent.WriteTo(writer, htmlEncoder);
            return writer.ToString();
        }
    }
}
