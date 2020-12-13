using probo_checker.DataAccess.Models;
using probo_checker.DataAccess.Repositories.Interfaces;

namespace probo_checker.Services.Implementations
{
    public class TestCaseService : BaseService<TestCase>
    {
        protected new readonly IRepository<TestCase> repository;

        public TestCaseService(IRepository<TestCase> newRepository) : base(newRepository)
        {
        }
    }
}
