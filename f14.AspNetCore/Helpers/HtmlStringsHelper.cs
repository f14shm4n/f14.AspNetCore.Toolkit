using System;
using System.Net;
using System.Text.RegularExpressions;

namespace f14.AspNetCore.Helpers
{
    /// <summary>
    /// Provides en extensions method for work with strings contains html data.
    /// </summary>
    public static class HtmlStringsHelper
    {
        /// <summary>
        /// Removes all html tags and truncates the given html string to desired length.
        /// </summary>
        /// <param name="htmlString">A string with html data.</param>
        /// <param name="length">The length for the output string.</param>
        /// <returns>The truncated string.</returns>
        public static string Truncate(string htmlString, int length)
        {
            if (string.IsNullOrWhiteSpace(htmlString))
            {
                return string.Empty;
            }

            string clearStr = RemoveTags(htmlString);
            if (clearStr.Length > length)
            {
                return clearStr.Truncate(length) + "...";
            }
            else
            {
                return clearStr;
            }
        }

        /// <summary>
        /// Removes all html tags from the given string.
        /// </summary>
        /// <param name="htmlString">A string contains html tags.</param>
        /// <returns>The string without tags.</returns>
        public static string RemoveTags(string htmlString) => WebUtility.HtmlDecode(Regex.Replace(htmlString, "<.*?>", string.Empty));
    }
}
