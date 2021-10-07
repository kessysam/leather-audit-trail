using System;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using ApplicationServices.Shared.Constants;
using Microsoft.Azure.Cosmos;
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

        public static async Task<AuditTrailService> InitializeCosmosClientInstanceAsync(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseName = configuration["DatabaseName"];
            var containerName = configuration["ContainerName"];
            var account = configuration["Account"];
            var key = configuration["Key"];
            var options = new CosmosClientOptions()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            };
            var client = new CosmosClient(account, key,options);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/applicationName");
            var cosmosDbService = new AuditTrailService(client, databaseName, containerName);
            return cosmosDbService;
        }
    }
}