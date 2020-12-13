using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace probo_checker.Common.CodeChecker.Models
{
    public class PostedSubmission
    {
        [Required]
        public string ProblemName { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public ICollection<TestCase> TestCases { get; set; }
    }
}