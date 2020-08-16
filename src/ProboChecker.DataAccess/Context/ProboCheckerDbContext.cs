using Microsoft.EntityFrameworkCore;
using ProboChecker.DataAccess.Models;

namespace ProboChecker.DataAccess.Context
{
    public class ProboCheckerDbContext : DbContext
    {
        public ProboCheckerDbContext(DbContextOptions<ProboCheckerDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApiKey> ApiKeys { get; set; }
    }
}