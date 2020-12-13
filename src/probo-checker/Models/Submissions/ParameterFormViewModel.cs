using probo_checker.Common.AutoMapper.Interfaces;
using probo_checker.Common.CodeChecker.Models;

namespace probo_checker.Models.Submissions
{
    public class ParameterFormViewModel : IMapWith<Parameter>
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}