using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using Domain;
using Microsoft.Azure.Cosmos;

namespace Persistence
{
    public class AuditTrailService : IAuditTrailService
    {
        private readonly Container _container;
        public AuditTrailService(CosmosClient cosmosDbClient, string databaseName, string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddAsync(ServiceAuditTrail item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.ApplicationName));
        }
        public async Task DeleteAsync(string applicationName)
        {
            await _container.DeleteItemAsync<ServiceAuditTrail>(applicationName, new PartitionKey(applicationName));
        }
        public async Task<ServiceAuditTrail> GetAsync(string applicationName)
        {
            try
            {
                var response = await _container.ReadItemAsync<ServiceAuditTrail>(applicationName, new PartitionKey(applicationName));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<ServiceAuditTrail>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<ServiceAuditTrail>(new QueryDefinition(queryString));
            var results = new List<ServiceAuditTrail>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateAsync(string applicationName, ServiceAuditTrail item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(applicationName));
        }
    }
}
