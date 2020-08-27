using f14.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace f14.Tests
{
    public class ClaimEqualityComparerTest
    {
        private ClaimTypeValueEqualityComparer GetComparer() => new ClaimTypeValueEqualityComparer();

        [Theory]
        [InlineData("a", "b", true)]
        [InlineData("a", "c", false)]
        public void List_Contains(string cType, string cValue, bool expected)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("a", "b"),
                new Claim("b", "c"),
            };

            var claim = new Claim(cType, cValue);

            Assert.Equal(expected, claims.Contains(claim, GetComparer()));
        }

        [Theory]
        [InlineData("a", "b", true)]
        [InlineData("a", "c", false)]
        public void HashSet_Add(string cType, string cValue, bool expected)
        {
            HashSet<Claim> claims = new HashSet<Claim>(GetComparer())
            {
                new Claim("a", "c")
            };

            Assert.Equal(expected, claims.Add(new Claim(cType, cValue)));
        }
    }
}
