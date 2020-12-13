using System;
using System.ComponentModel.DataAnnotations;

namespace probo_checker.DataAccess.Models
{
    public class ApiKey : BaseEntity
    {
        [Required]
        public string Key { get; set; }

        public DateTime Created { get; set; }
    }
}
