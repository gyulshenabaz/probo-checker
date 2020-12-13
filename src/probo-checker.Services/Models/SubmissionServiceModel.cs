using System.Collections.Generic;
using AutoMapper;
using probo_checker.Common.AutoMapper.Interfaces;
using probo_checker.Common.CodeChecker.Models;
using probo_checker.DataAccess.Models;

namespace probo_checker.Services.Models
{
    public class SubmissionServiceModel : ICustomMapper
    {
        public string ProblemName { get; set; }
        
        public string Code { get; set; }
        
        public string Message { get; set; }
        
        public string Language { get; set; }

        public double Score { get; set; }

        public ICollection<TestCaseServiceModel> Tests { get; set; }
        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<SubmissionServiceModel, Submission>();
            mapper.CreateMap<SubmissionResult,SubmissionServiceModel >();
        }
    }
}