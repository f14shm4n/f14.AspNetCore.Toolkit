using Microsoft.AspNetCore.Identity;
using System;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Provides a abstract class for identity data updater. Also, provides specific implementations.
    /// </summary>
    public class IdentityUpdater
    {
        /// <summary>
        /// Fired when <see cref = "IdentityResult" /> returned with an error state.
        /// </summary>
        public event EventHandler<IdentityResult>? IdentityError;

        /// <summary>
        /// Performs the <see cref="IdentityError"/> with each Identity error if it has occurred.
        /// </summary>
        /// <param name="identityResult">The identity result of some operation.</param>
        protected virtual void RaiseIdentityErrors(IdentityResult identityResult) => IdentityError?.Invoke(this, identityResult);
    }
}
