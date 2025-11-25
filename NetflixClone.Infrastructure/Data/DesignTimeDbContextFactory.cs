using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.IO;

namespace NetflixClone.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            var basePath = Directory.GetCurrentDirectory();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(basePath, "appsettings.json"), optional: false, reloadOnChange: true)
                .AddJsonFile(Path.Combine(basePath, "appsettings.Development.json"), optional: true)
                .Build();

            // Build DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                // Fallback connection string for design-time operations
                connectionString = "Server=(localdb)\\mssqllocaldb;Database=NetflixCloneDb;Trusted_Connection=true;MultipleActiveResultSets=true";
            }

            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}