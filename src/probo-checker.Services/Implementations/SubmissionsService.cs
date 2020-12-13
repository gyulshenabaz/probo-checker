using AutoMapper;
using probo_checker.Common.CodeChecker.Implementations;
using probo_checker.Common.CodeChecker.Models;
using probo_checker.DataAccess.Repositories.Interfaces;
using probo_checker.Services.Interfaces;
using probo_checker.Services.Models;
using Submission = probo_checker.DataAccess.Models.Submission;

namespace probo_checker.Services.Implementations
{
    public class SubmissionsService : BaseService<Submission>, ISubmissionsService
    {
        public SubmissionsService(IRepository<Submission> repository) : base(repository)
        {

        }

        public SubmissionResult Create(PostedSubmission model)
        {
            var codeChecker = BaseCodeChecker.GetCodeChecker(model);

            var resultModel = codeChecker.Process();

            var serviceModel = Mapper.Map<SubmissionServiceModel>(resultModel);

            Create(serviceModel);

            return resultModel;
        }

        private void Create(SubmissionServiceModel serviceModel)
        {
            var submission = Mapper.Map<Submission>(serviceModel);

            this.repository.Add(submission);

            this.repository.SaveChanges();
        }
    }
}
