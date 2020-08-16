using System;
using System.Linq;
using ProboChecker.DataAccess.Models;
using ProboChecker.DataAccess.Repository.Interfaces;
using ProboChecker.Services.Interfaces;

namespace ProboChecker.Services.Implementations
{
    public class ApiKeysService : BaseService, IApiKeysService
    {
        private readonly IRepository<ApiKey> apiKeysRepository;
        private readonly IRandomKeyGenerator randomKeyGenerator;

        public ApiKeysService(IRepository<ApiKey> apiKeysRepository,
            IRandomKeyGenerator randomKeyGenerator)
        {
            this.apiKeysRepository = apiKeysRepository;
            this.randomKeyGenerator = randomKeyGenerator;
        }

        public string Generate()
        {
            string generatedKey;

            while (true)
            {
                generatedKey = this.randomKeyGenerator.GenerateRandomKey(byteLength: 32);
                var key = generatedKey;
                if (!IsKeyValid(key))
                {
                    break;
                }
            }

            var apiKey = new ApiKey
            {
                Key = generatedKey,
                Created = DateTime.Now
            };

            this.apiKeysRepository.Add(apiKey);

            this.apiKeysRepository.SaveChanges();

            return generatedKey;
        }

        public bool IsKeyValid(string apiKey)
            => this.apiKeysRepository.All()
                .AsEnumerable()
                .Any(ak => ak.Key == apiKey);
    }
}