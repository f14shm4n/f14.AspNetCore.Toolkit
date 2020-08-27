namespace System.Security.Claims
{
    /// <summary>
    /// Provides an extensions methods for claims.
    /// </summary>
    public static class ClaimsExtension
    {
        /// <summary>
        /// Searches for claim value by <see cref="ClaimTypes.NameIdentifier"/>.
        /// </summary>
        /// <param name="principal">The user claim principal.</param>
        /// <returns>The user id or null.</returns>        
        public static string? GetUserId(this ClaimsPrincipal principal) => principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        /// <summary>
        /// Searches for claim value by <see cref="ClaimTypes.Name"/>.
        /// </summary>
        /// <param name="principal">The user claim principal.</param>
        /// <returns>The user name or null.</returns>
        public static string? GetUserName(this ClaimsPrincipal principal) => principal.FindFirst(ClaimTypes.Name)?.Value;
    }
}
