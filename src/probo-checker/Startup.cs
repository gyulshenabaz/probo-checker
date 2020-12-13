using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using probo_checker.Common.AutoMapper;
using probo_checker.DataAccess.Context;
using probo_checker.DataAccess.Repositories.Implementations;
using probo_checker.DataAccess.Repositories.Interfaces;
using probo_checker.Services.Helpers;
using probo_checker.Services.Implementations;
using probo_checker.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace probo_checker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProboCheckerDbContext>(options =>
                     options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IApiKeysService, ApiKeysService>();
            services.AddScoped<ISubmissionsService, SubmissionsService>();

            services.AddScoped<IRandomKeyGenerator, RandomKeyGenerator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Probo Checker", Version = "v1" });
                c.AddSecurityDefinition("APIKeyQueryParam",
                    new ApiKeyScheme
                    {
                        In = "query",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"APIKeyQueryParam", Enumerable.Empty<string>() },
                };
                c.AddSecurityRequirement(security);

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Probo Checker V1");
            });

            app.UseMvc();
        }
    }
}
