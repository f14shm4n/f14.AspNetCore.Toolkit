using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Provides a user creating\updating method for identity users.
    /// </summary>
    /// <typeparam name="TUserManager">Type of the user manager.</typeparam>
    /// <typeparam name="TUser">Type of the identity user.</typeparam>
    /// <typeparam name="TUserInfo">Type of the user info.</typeparam>
    public class UserUpdater<TUserManager, TUser, TUserInfo> : IdentityUpdater
        where TUserManager : UserManager<TUser>
        where TUser : class
        where TUserInfo : UserInfo
    {
        /// <summary>
        /// Updates identity users with specified user manager and users data.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="users">A set of user data that should be used to create or update users.</param>
        /// <param name="userFactory">A user record factory method.</param>
        /// <returns>An asynchronous operation.</returns>
        public virtual async Task UpdateAsync(TUserManager userManager, IEnumerable<TUserInfo> users, Func<TUserInfo, TUser> userFactory)
        {
            IdentityResult result;
            foreach (var userInfo in users)
            {
                var user = await userManager.FindByNameAsync(userInfo.Login);
                if (user == null)
                {
                    user = userFactory(userInfo);
                    result = await userManager.CreateAsync(user, userInfo.Password);
                    if (!result.Succeeded)
                    {
                        RaiseIdentityErrors(result);
                        continue;
                    }
                }

                if (userInfo.Roles != null)
                {
                    await UpdateRolesAsync(userManager, user, userInfo.Roles);
                }
            }
        }

        /// <summary>
        /// Updates user roles.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="user">The concrete user.</param>
        /// <param name="roles">The roles collection to update.</param>
        /// <returns>An asynchronous operation.</returns>
        private async Task UpdateRolesAsync(TUserManager userManager, TUser user, IEnumerable<string> roles)
        {
            var currentRoles = await userManager.GetRolesAsync(user);
            var newRoles = roles.Except(currentRoles);

            if (newRoles.Count() > 0)
            {
                var result = await userManager.AddToRolesAsync(user, newRoles);
                if (!result.Succeeded)
                {
                    RaiseIdentityErrors(result);
                }
            }
        }
    }
}
