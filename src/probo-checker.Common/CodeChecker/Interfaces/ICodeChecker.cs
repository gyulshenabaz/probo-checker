using probo_checker.Common.CodeChecker.Models;

namespace probo_checker.Common.CodeChecker.Interfaces
{
    public interface ICodeChecker
    {
        SubmissionResult Process();
    }
}