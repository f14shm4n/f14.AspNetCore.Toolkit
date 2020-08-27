using System.Collections.Generic;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Represents the user proxy class.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Creates new instance of the user info.
        /// </summary>
        /// <param name="login">User login (user name).</param>
        /// <param name="password">User password.</param>
        /// <param name="roles">User roles.</param>
        public UserInfo(string login, string password, IEnumerable<string>? roles) 
            : this(login, password, null, roles)
        {
        }

        /// <summary>
        /// Creates new instance of the user info.
        /// </summary>
        /// <param name="login">User login (user name).</param>
        /// <param name="password">User password.</param>
        /// <param name="email">User email.</param>
        /// <param name="roles">User roles.</param>
        public UserInfo(string login, string password, string? email, IEnumerable<string>? roles)
        {
            Login = login;
            Password = password;
            Email = email;
            Roles = new HashSet<string>();

            if (roles != null)
            {
                foreach (var r in roles)
                {
                    Roles.Add(r);
                }
            }
        }

        /// <summary>
        /// User login.
        /// </summary>
        public string Login { get; private set; }

        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// User email.
        /// </summary>
        public string? Email { get; private set; }

        /// <summary>
        /// User roles.
        /// </summary>
        public HashSet<string> Roles { get; private set; }
    }
}
