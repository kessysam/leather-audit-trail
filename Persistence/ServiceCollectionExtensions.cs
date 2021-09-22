using System;
using ApplicationServices.Interfaces;
using ApplicationServices.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Persistence
{
	public  static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString( DbConnectionStrings.AuditTrailDbConnection);

			services.AddDbContextPool<AuditTrailDbContext>(options =>
				options.UseSqlServer(connectionString,
					b =>
                    {
                        b.MigrationsAssembly(typeof(AuditTrailDbContext).Assembly.FullName);
                        b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10),null);
                    }));

			services.TryAddScoped<IAuditTrailDbContext>(provider => provider.GetService<AuditTrailDbContext>());

			return services;
		}
	}
}