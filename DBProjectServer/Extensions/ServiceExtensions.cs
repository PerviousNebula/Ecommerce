using Contracts;
using DBProject.ActionFilters;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace DBProject.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination")
                );
            });
        }

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

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureEntityExistsAttribute(this IServiceCollection services)
        {
            services.AddScoped<ValidateEntityExistsAttribute<Customer>>();
            services.AddScoped<ValidateEntityExistsAttribute<Address>>();
            services.AddScoped<ValidateEntityExistsAttribute<Category>>();
            services.AddScoped<ValidateEntityExistsAttribute<Product>>();
            services.AddScoped<ValidateEntityExistsAttribute<Size>>();
            services.AddScoped<ValidateEntityExistsAttribute<Color>>();
            services.AddScoped<ValidateEntityExistsAttribute<User>>();
            services.AddScoped<ValidateEntityExistsAttribute<Order>>();
            services.AddScoped<ValidateEntityExistsAttribute<OrderDetail>>();
            services.AddScoped<ValidateEntityExistsAttribute<ProductDesign>>();
        }
    }
}