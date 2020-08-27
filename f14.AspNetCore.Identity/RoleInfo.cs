using System.Collections.Generic;
using System.Security.Claims;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Provides basic data that represent some identity role.
    /// </summary>
    public class RoleInfo
    {
        /// <summary>
        /// Creates new instance for role info.
        /// </summary>
        /// <param name="name">Role name.</param>
        public RoleInfo(string name)
            : this(name, null)
        {
        }

        /// <summary>
        /// Creates new instance for role info.
        /// </summary>
        /// <param name="name">Role name.</param>
        /// <param name="claims">Role claims.</param>
        public RoleInfo(string name, IEnumerable<Claim>? claims)
        {
            Name = name;
            Claims = new HashSet<Claim>(new ClaimTypeValueEqualityComparer());

            if (claims != null)
            {
                foreach (var c in claims)
                {
                    Claims.Add(c);
                }
            }
        }

        /// <summary>
        /// Provides the name of the role.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Provides a set of the claims.
        /// </summary>
        public HashSet<Claim> Claims { get; private set; }
    }
}
