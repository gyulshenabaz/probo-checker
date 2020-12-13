using probo_checker.Services.Interfaces;
using System;
using System.Security.Cryptography;

namespace probo_checker.Services.Helpers
{
    public class RandomKeyGenerator : IRandomKeyGenerator
    {
        public string GenerateRandomKey(int byteLength)
        {
            var key = new byte[byteLength];

            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(key);
            }

            string randomKey = Convert.ToBase64String(key);

            return new string(randomKey);
        }
    }
}
