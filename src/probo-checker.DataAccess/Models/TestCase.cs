using System.Collections.Generic;

namespace probo_checker.DataAccess.Models
{
    public class TestCase : BaseEntity
    {
        public string ExpectedOutput { get; set; }

        public string ActualOutput { get; set; }
        
        public ICollection<Parameter> Parameters { get; set; }

        public int SubmissionId { get; set; }
        public Submission Submission { get; set; }
    }
}
