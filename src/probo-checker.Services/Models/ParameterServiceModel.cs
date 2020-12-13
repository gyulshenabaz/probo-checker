using AutoMapper;
using probo_checker.Common.AutoMapper.Interfaces;
using probo_checker.DataAccess.Models;

namespace probo_checker.Services.Models
{
    public class ParameterServiceModel : ICustomMapper
    {
        
        public string Name { get; set; }
        
        public string Type { get; set; }
        
        public string Value { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<ParameterServiceModel, Parameter>();
            mapper.CreateMap<Common.CodeChecker.Models.Parameter,ParameterServiceModel>();
        }
    }
}