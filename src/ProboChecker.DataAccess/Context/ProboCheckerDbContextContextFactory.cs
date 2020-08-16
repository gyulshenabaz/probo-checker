using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProboChecker.DataAccess.Context
{
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
            builder.UseSqlServer(connectionString);

            return new ProboCheckerDbContext(builder.Options);
        }
    }
}