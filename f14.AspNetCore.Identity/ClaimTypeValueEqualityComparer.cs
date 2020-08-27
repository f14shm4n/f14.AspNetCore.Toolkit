using System.Collections.Generic;
using System.Security.Claims;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Provides an implementation of the <see cref="IEqualityComparer{T}"/> for <see cref="Claim"/> object 
    /// which compares objects by <see cref="Claim.Type"/> and <see cref="Claim.Value"/>.
    /// </summary>
    public sealed class ClaimTypeValueEqualityComparer : EqualityComparer<Claim>
    {
        ///<inheritdoc/>
        public override bool Equals(Claim x, Claim y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return x.Type == y.Type && x.Value == y.Value;
        }

        ///<inheritdoc/>
        public override int GetHashCode(Claim obj) => (obj.Type.GetHashCode() ^ obj.Value.GetHashCode()).GetHashCode();
    }
}
