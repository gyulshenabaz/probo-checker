using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProboChecker.DataAccess.Context;
using ProboChecker.DataAccess.Repository;
using ProboChecker.DataAccess.Repository.Interfaces;
using ProboChecker.Services.Helpers;
using ProboChecker.Services.Implementations;
using ProboChecker.Services.Interfaces;

namespace ProboChecker.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }        
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProboCheckerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IApiKeysService, ApiKeysService>();
            services.AddScoped<IRandomKeyGenerator, RandomKeyGenerator>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
            
            app.UseRouting();
        }
    }
}