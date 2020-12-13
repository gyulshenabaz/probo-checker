using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using probo_checker.Common.AutoMapper.Interfaces;
using probo_checker.Common.CodeChecker.Models;
using probo_checker.Services.Implementations;
using probo_checker.Services.Models;

namespace probo_checker.Models.Submissions
{
    public class TestCaseFormViewModel : IMapWith<TestCase>
    {
        [Required]
        public List<ParameterFormViewModel> Parameters { get; set; }

        [Required]
        public string ExpectedOutput { get; set; }
    }
}