using System.Collections.Generic;

namespace probo_checker.Common.CodeChecker.Models
{
    public class Test
    {
        public string ExpectedOutput { get; set; }

        public string ActualOutput { get; set; }

        public bool Passed { get; set; }

        public ICollection<Parameter> Parameters { get; set; }
    }
}