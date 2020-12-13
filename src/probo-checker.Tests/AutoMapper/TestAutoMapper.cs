using AutoMapper;
using probo_checker.Common.AutoMapper;

namespace probo_checker.Tests.AutoMapper
{
    public static class TestAutoMapper
    {
        private static bool isInitialized;

        public static void InitializeAutoMapper()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;

            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
        }
    }
}
