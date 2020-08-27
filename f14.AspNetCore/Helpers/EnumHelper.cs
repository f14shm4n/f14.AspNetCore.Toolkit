using f14.AspNetCore.Extensions;
using Microsoft.AspNetCore.Html;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace f14.AspNetCore.Helpers
{
    /// <summary>
    /// Provides an extensions methods for work with enum.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets a <see cref="DisplayAttribute"/> for enum field..
        /// </summary>
        /// <param name="enumValue">An enum field.</param>
        /// <returns>The <see cref="DisplayAttribute"/> object or null.</returns>
        public static DisplayAttribute? GetDisplayAttribute(object enumValue)
        {
            enumValue.ThrowIfNull(nameof(enumValue));

            var fi = enumValue.GetType().GetTypeInfo().GetField(enumValue.ToString());
            return fi?.GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();
        }

        /// <summary>
        /// Extracts the <see cref="DisplayAttribute.Name"/> and create new <see cref="HtmlString"/>.
        /// </summary>
        /// <param name="enumValue">The enum field.</param>
        /// <returns>The html content.</returns>
        public static IHtmlContent DisplayFor(object enumValue)
        {
            enumValue.ThrowIfNull(nameof(enumValue));

            return GetDisplayAttribute(enumValue)?.Name.AsHtml() ?? new HtmlString(enumValue.ToString());
        }
    }
}
