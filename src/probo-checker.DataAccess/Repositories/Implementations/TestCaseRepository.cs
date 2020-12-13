using probo_checker.DataAccess.Context;
using probo_checker.DataAccess.Models;
using probo_checker.DataAccess.Repositories.Interfaces;

namespace probo_checker.DataAccess.Repositories.Implementations
{
    public class TestCaseRepository : Repository<TestCase>, ITestCaseRepository
    {
        public TestCaseRepository(ProboCheckerDbContext context)
            : base(context)
        { }
    }
}
