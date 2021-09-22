using System;
using System.IO;
using System.Reflection;
using ApplicationServices.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AuditTrailDbContext>
	{
		public AuditTrailDbContext CreateDbContext(string[] args)
		{
			var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			var configuration = new ConfigurationBuilder()
			                    .SetBasePath(Directory.GetCurrentDirectory())
			                    .AddJsonFile("secrets.json", optional: true)
			                    .AddJsonFile("appsettings.json", optional: true)
			                    .AddJsonFile($"appsettings.{envName}.json", optional: true)
			                    .AddUserSecrets(Assembly.GetExecutingAssembly(), optional:true)
			                    .Build();

			var builder = new DbContextOptionsBuilder<AuditTrailDbContext>();
			var connectionString = configuration.GetConnectionString(DbConnectionStrings.AuditTrailDbConnection);

			builder.UseSqlServer(connectionString);

			return new AuditTrailDbContext(builder.Options);
		}
	}
}