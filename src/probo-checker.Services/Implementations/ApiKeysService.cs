using probo_checker.DataAccess.Models;
using probo_checker.DataAccess.Repositories.Interfaces;
using probo_checker.Services.Interfaces;
using System;
using System.Linq;

namespace probo_checker.Services.Implementations
{
    public class ApiKeysService : BaseService<ApiKey>, IApiKeysService
    {
        private readonly IRepository<ApiKey> apiKeysRepository;
        private readonly IRandomKeyGenerator randomKeyGenerator;

        public ApiKeysService(IRepository<ApiKey> apiKeysRepository,
            IRandomKeyGenerator randomKeyGenerator) : base(apiKeysRepository)
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
                if (!IsValidKey(key))
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

        public bool IsValidKey(string apiKey)
            => this.apiKeysRepository.All().AsEnumerable().Any(ak => ak.Key == apiKey);
    }
}
