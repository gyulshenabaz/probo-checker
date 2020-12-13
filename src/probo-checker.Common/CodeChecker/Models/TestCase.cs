using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace probo_checker.Common.CodeChecker.Models
{
    public class TestCase
    {
        [Required]
        public string ExpectedOutput { get; set; }
        
        [Required]
        public ICollection<Parameter> Parameters { get; set; }
    }
}