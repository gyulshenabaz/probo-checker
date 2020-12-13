using System.Collections.Generic;

namespace probo_checker.DataAccess.Models
{
    public class Submission : BaseEntity
    {
        public string ProblemName { get; set; }
        
        public string Message { get; set; }
        
        public string Language { get; set; }

        public double Score { get; set; }

        public ICollection<TestCase> Tests { get; set; }
    }
}
