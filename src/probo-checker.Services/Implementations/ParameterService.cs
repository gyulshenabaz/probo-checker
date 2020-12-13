

using probo_checker.DataAccess.Models;
using probo_checker.DataAccess.Repositories.Interfaces;

namespace probo_checker.Services.Implementations
{
    public class ParameterService : BaseService<Parameter>
    {
        protected new readonly IRepository<Parameter> repository;

        public ParameterService(IRepository<Parameter> newRepository) : base(newRepository)
        {
        }
    }
}
