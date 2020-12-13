using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using probo_checker.DataAccess.Models;
using System.IO;

namespace probo_checker.DataAccess.Context
{
    public class ProboCheckerDbContext : DbContext
    {
        public ProboCheckerDbContext(DbContextOptions<ProboCheckerDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Submission> Submission { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
    }

    public class ProboCheckerDbContextContextFactory : IDesignTimeDbContextFactory<ProboCheckerDbContext>
    {
        public ProboCheckerDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ProboCheckerDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseMySQL(connectionString);

            return new ProboCheckerDbContext(builder.Options);
        }
    }
}