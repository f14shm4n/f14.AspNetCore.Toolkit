using f14.AspNetCore.Helpers;
using f14.xunit;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace f14.Tests
{
    public class HashHelperTest : XUnitTestBase
    {
        public HashHelperTest(ITestOutputHelper logger) : base(logger)
        {
        }

        [Theory]
        [InlineData("Sample data for hash.", "Sample hash salt.")]
        [InlineData("Тестовые данные для хэша.", "Тестовая соль для хэша.")]
        public void ComputeAndValidate(string data, string salt)
        {
            var hash = HashHelper.ComputePbkdf2(data, Encoding.Unicode.GetBytes(salt));

            Logger.WriteLine($"Text: {data}\nHash: {hash}");

            Assert.NotEqual(data, hash);

            Assert.True(HashHelper.ValidatePbkdf2(hash, data, Encoding.Unicode.GetBytes(salt)));
        }
    }
}
