using Microsoft.EntityFrameworkCore;
using probo_checker.DataAccess.Context;
using probo_checker.Tests.AutoMapper;
using System;

namespace probo_checker.Tests.ServicesTests
{
    public abstract class BaseTest
    {
        protected BaseTest()
        {
            TestAutoMapper.InitializeAutoMapper();
        }

        protected ProboCheckerDbContext DatabaseInstance
        {
            get
            {
                var options = new DbContextOptionsBuilder<ProboCheckerDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging()
                    .Options;

                return new ProboCheckerDbContext(options);
            }
        }
    }
}
