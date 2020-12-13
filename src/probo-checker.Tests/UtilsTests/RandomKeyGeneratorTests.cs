using probo_checker.Services.Helpers;
using System.Collections.Generic;
using Xunit;

namespace probo_checker.Tests.UtilsTests
{
    public class RandomKeyGeneratorTests
    {
        [Fact]
        public void GenerateRandomKey_ReturnsRandomResults()
        {
            // Arrange
            const int iterations = 100;
            const int byteLength = 32;

            var randomKeyGenerator = new RandomKeyGenerator();

            var results = new HashSet<string>();

            // Act
            for (int i = 0; i < iterations; i++)
            {
                var result = randomKeyGenerator.GenerateRandomKey(byteLength);
                results.Add(result);
            }

            // Assert
            Assert.Equal(iterations, results.Count);
        }
    }
}
