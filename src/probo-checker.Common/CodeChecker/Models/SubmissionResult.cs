using System.Collections.Generic;

namespace probo_checker.Common.CodeChecker.Models
{
    public class SubmissionResult
    {
        public string ProblemName { get; set; }
        public string Message { get; set; }
        
        public string Language { get; set; }

        public double Score { get; set; }

        public ICollection<Test> Tests { get; set; }
    }
}