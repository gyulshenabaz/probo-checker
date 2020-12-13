using probo_checker.Common.AutoMapper.Interfaces;
using probo_checker.DataAccess.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace probo_checker.Services.Models
{
    public class ApiKeyServiceModel : IMapWith<ApiKey>
    {
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        public DateTime Created { get; set; }
    }
}
