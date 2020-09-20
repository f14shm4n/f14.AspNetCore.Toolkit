using System;

namespace f14.AspNetCore.Mvc.Extensions
{
    /// <summary>
    /// Provides an extensions methods for <see cref="string"/> type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Extracts the Asp.Net Core controller name from current string value. This method removes the string entry 'Controller' from source string.
        /// </summary>
        /// <param name="source">The source value as controller name.</param>
        /// <returns>The string value which contains substring of source string without 'Controller' token.</returns>
        public static string GetControllerName(this string source) => source.Replace("Controller", "", StringComparison.OrdinalIgnoreCase);
    }
}
