using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace f14.AspNetCore.Helpers
{
    /// <summary>
    /// Provides helper methods for computing hash for the string data.
    /// </summary>
    public static class HashHelper
    {
        /// <summary>
        /// Generates new hash value using PBKDF2 algorithm as base64 encoded string.
        /// </summary>
        /// <param name="data">The string data for hashing.</param>
        /// <param name="salt">The hash salt.</param>
        /// <param name="iterationCount">The number of iterations of the pseudo-random function to apply during the key derivation process.</param>
        /// <param name="numBytesRequested">The desired length (in bytes) of the derived key.</param>
        /// <returns>The computed hash for given data.</returns>
        public static string ComputePbkdf2(string data, byte[] salt, int iterationCount = 1000, int numBytesRequested = 256 / 8)
        {
            var hashedBytes = KeyDerivation.Pbkdf2(data, salt, KeyDerivationPrf.HMACSHA256, iterationCount, numBytesRequested);
            var hashedString = Convert.ToBase64String(hashedBytes);
            return hashedString;
        }

        /// <summary>
        /// Validates a hash value which generated with PBKDF2 algorithm.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="data">The string data.</param>
        /// <param name="salt">The hash salt.</param>
        /// <param name="iterationCount">The number of iterations of the pseudo-random function to apply during the key derivation process.</param>
        /// <param name="numBytesRequested">The desired length (in bytes) of the derived key.</param>
        /// <returns>True - if given data is match with given hash; False - otherwise.</returns>
        public static bool ValidatePbkdf2(string hash, string data, byte[] salt, int iterationCount = 1000, int numBytesRequested = 256 / 8)
        {
            return string.Equals(hash, ComputePbkdf2(data, salt, iterationCount, numBytesRequested));
        }

        /// <summary>
        /// Generates random salt.
        /// </summary>
        /// <param name="length">The salt length.</param>
        /// <returns>The salt as byte array.</returns>
        public static byte[] GenerateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
