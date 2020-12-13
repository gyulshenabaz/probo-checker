using probo_checker.DataAccess.Context;
using probo_checker.DataAccess.Models;
using probo_checker.DataAccess.Repositories.Interfaces;

namespace probo_checker.DataAccess.Repositories.Implementations
{
    public class SubmissionRepository : Repository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(ProboCheckerDbContext context)
            : base(context)
        { }
    }
}
