namespace probo_checker.DataAccess.Models
{
    public class Parameter : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        
        public int TestCaseId { get; set; }
        public TestCase TestCase { get; set; }
    }
}
