using System.Collections.Generic;
using AutoMapper;
using probo_checker.Common.AutoMapper.Interfaces;
using probo_checker.Common.CodeChecker.Models;
using TestCase = probo_checker.DataAccess.Models.TestCase;

namespace probo_checker.Services.Models
{
    public class TestCaseServiceModel : ICustomMapper
    {
        public string ExpectedOutput { get; set; }

        public string ActualOutput { get; set; }
        
        public ICollection<ParameterServiceModel> Parameters { get; set; }
        
        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<TestCaseServiceModel, TestCase>();
            mapper.CreateMap<Test,TestCaseServiceModel>();
        }
    }
}