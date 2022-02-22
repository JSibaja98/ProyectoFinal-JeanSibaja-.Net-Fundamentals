using Microsoft.Azure.Cosmos;
using ProyectoFinal_JeanSibaja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_JeanSibaja.Services
{
    public interface ICosmosDBService<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(string query);
        Task<T> GetItemAsync(string id);
        Task AddItemAsync(T item, string id);
        Task UpdateItemAsync(string id, T item);
        Task DeleteItemAsync(string id);
    }

    public class CosmosDBService<T> : ICosmosDBService<T>
    {
        private Container _container;
        public CosmosDBService(CosmosClient dbClient, string dataBaseName, string containerName)
        {
            this._container = dbClient.GetContainer(dataBaseName, containerName);
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<T> response = await this._container.ReadItemAsync<T>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(T);
            }
        }

        public async Task AddItemAsync(T item, string id)
        {
            await this._container.CreateItemAsync<T>(item, new PartitionKey(id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<T>(id, new PartitionKey(id));
        }

        public async Task UpdateItemAsync(string id, T item)
        {
            await this._container.UpsertItemAsync<T>(item, new PartitionKey(id));
        }
    }
}
