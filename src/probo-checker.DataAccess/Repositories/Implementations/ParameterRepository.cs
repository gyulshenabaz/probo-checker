using probo_checker.DataAccess.Context;
using probo_checker.DataAccess.Models;
using probo_checker.DataAccess.Repositories.Interfaces;

namespace probo_checker.DataAccess.Repositories.Implementations
{
    public class ParameterRepository : Repository<Parameter>, IParameterRepository
    {
        public ParameterRepository(ProboCheckerDbContext context)
            : base(context)
        { }
    }
}
