using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using probo_checker.Common.AutoMapper.Interfaces;
using probo_checker.Common.CodeChecker.Models;
using probo_checker.Services.Implementations;

namespace probo_checker.Models.Submissions
{
    public class SubmissionFormViewModel : IMapWith<PostedSubmission>
    {
        [Required] 
        public string ProblemName { get; set; }

        [Required]
        public string Code { get; set; }

        [Required] 
        public string Language { get; set; }

        [Required]
        public List<TestCaseFormViewModel> TestCases { get; set; }
    }
}