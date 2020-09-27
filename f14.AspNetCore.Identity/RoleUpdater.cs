using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Provides a role creating\updating method for identity roles.
    /// </summary>
    /// <typeparam name="TRole">Type of the identity role object.</typeparam>
    /// <typeparam name="TRoleInfo">Type of the role info object.</typeparam>
    public class RoleUpdater<TRole, TRoleInfo> : IdentityUpdater
        where TRole : class
        where TRoleInfo : RoleInfo
    {
        /// <summary>
        /// Updates identity roles with specified role manager and roles data.
        /// </summary>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="roles">The role collection that to need to be update in database.</param>
        /// <param name="roleFactory">The role factory which uses to create role object that will be inserted to the database.</param>
        /// <returns>An asynchronous operation.</returns>
        public virtual async Task UpdateAsync(RoleManager<TRole> roleManager, IEnumerable<TRoleInfo> roles, Func<TRoleInfo, TRole> roleFactory)
        {
            IdentityResult result;
            foreach (var roleInfo in roles)
            {
                bool exist = await roleManager.RoleExistsAsync(roleInfo.Name);
                if (!exist)
                {
                    var role = roleFactory(roleInfo);
                    result = await roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        foreach (var claim in roleInfo.Claims)
                        {
                            result = await roleManager.AddClaimAsync(role, claim);
                            if (!result.Succeeded)
                            {
                                RaiseIdentityErrors(result);
                            }
                        }
                    }
                    else
                    {
                        RaiseIdentityErrors(result);
                    }
                }
                else
                {
                    var role = await roleManager.FindByNameAsync(roleInfo.Name);
                    var claims = await roleManager.GetClaimsAsync(role);
                    var claimComparer = new ClaimTypeValueEqualityComparer();

                    foreach (var claim in roleInfo.Claims)
                    {
                        if (!claims.Contains(claim, claimComparer))
                        {
                            result = await roleManager.AddClaimAsync(role, claim);
                            if (!result.Succeeded)
                            {
                                RaiseIdentityErrors(result);
                            }
                        }
                    }
                }
            }
        }
    }
}
