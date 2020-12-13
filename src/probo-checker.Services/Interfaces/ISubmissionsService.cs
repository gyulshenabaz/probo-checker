using probo_checker.Common.CodeChecker.Models;
using probo_checker.DataAccess.Models;
using probo_checker.Services.Models;

namespace probo_checker.Services.Interfaces
{
    public interface ISubmissionsService
    {
        SubmissionResult Create(PostedSubmission model);
    }
}