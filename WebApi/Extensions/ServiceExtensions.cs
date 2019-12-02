using BLL;
using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure Cross-Origin Resource Sharing
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin() // or WithOrigins("https://toto.com")
                    .AllowAnyMethod() // Or WithMethods("POST, "GET")
                    .AllowAnyHeader() // Or WithHeaders("accept", "content-type")
                    .AllowCredentials());
            });
        }

        /// <summary>
        /// Configure IIS Integration
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString));
        }

        public static void RegisterDependencies(this IServiceCollection services)
        {
            DependenciesManager.RegisterDependencies(services);
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Info()
                {
                    Version = "v1",
                    Title = "Swagger API",
                });
            });
        }
    }
}
