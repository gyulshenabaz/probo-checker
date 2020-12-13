using Moq;
using probo_checker.DataAccess.Context;
using probo_checker.DataAccess.Models;
using probo_checker.DataAccess.Repositories.Implementations;
using probo_checker.Services.Implementations;
using probo_checker.Services.Interfaces;
using System;
using System.Linq;
using Xunit;

namespace probo_checker.Tests.ServicesTests
{
    public class ApiKeysServiceTests : BaseTest
    {
        private readonly ProboCheckerDbContext context;
        private readonly Mock<IRandomKeyGenerator> mockRandomKeyGenerator;
        private readonly ApiKeysService service;

        public ApiKeysServiceTests()
        {
            this.context = this.DatabaseInstance;
            this.mockRandomKeyGenerator = new Mock<IRandomKeyGenerator>();
            this.service = new ApiKeysService(new Repository<ApiKey>(context), mockRandomKeyGenerator.Object);
        }

        [Fact]
        public void GenerateAsync_WithNonExistingKey_WorksCorrectly()
        {
            // Arrange
            const string expectedKey = "expkey";

            mockRandomKeyGenerator.Setup(r => r.GenerateRandomKey(It.IsAny<int>())).Returns(expectedKey);

            // Act
            var resultKey = service.Generate();

            // Assert
            Assert.Equal(expectedKey, resultKey);

            var apiKeyExists = context.ApiKeys.Any(ak => ak.Key == expectedKey);
            Assert.True(apiKeyExists);
        }

        [Fact]
        public void GenerateAsync_WithExistingKeyGenerated_WorksCorrectly()
        {
            // Arrange
            const string existingKey = "exikey";
            const string newKey = "newkey";

            context.ApiKeys.Add(new ApiKey
            {
                Key = existingKey,
                Created = DateTime.Now
            });

            context.SaveChanges();

            mockRandomKeyGenerator.SetupSequence(r => r.GenerateRandomKey(It.IsAny<int>()))
                .Returns(existingKey)
                .Returns(newKey);

            // Act
            var resultKey = service.Generate();

            // Assert
            Assert.Equal(newKey, resultKey);

            mockRandomKeyGenerator.Verify(r => r.GenerateRandomKey(It.IsAny<int>()), Times.Exactly(2));

            var apiKeyExists = context.ApiKeys.Any(ak => ak.Key == resultKey);
            Assert.True(apiKeyExists);
        }

        [Fact]
        public void IsValidKey_WithExistingKey_ReturnsTrue()
        {
            const string testKey = "testKey";

            mockRandomKeyGenerator.Setup(r => r.GenerateRandomKey(It.IsAny<int>())).Returns(testKey);

            // Act
            var resultKey = service.Generate();

            var keyExists = service.IsValidKey(resultKey);

            Assert.True(keyExists);
        }

        [Fact]
        public void IsValidKey_WithExistingKey_ReturnsFalse()
        {
            const string testKey = "testKey";

            var keyExists = service.IsValidKey(testKey);

            Assert.False(keyExists);
        }
    }
}
