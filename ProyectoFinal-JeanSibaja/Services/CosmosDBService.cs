using Microsoft.Azure.Cosmos;
using ProyectoFinal_JeanSibaja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_JeanSibaja.Services
{
    public interface ICosmosDBServiceMaquina
    {
        Task<IEnumerable<Maquina>> GetItemsAsync(string query);
        Task<Maquina> GetItemAsync(string id);
        Task AddItemAsync(Maquina item, string id);
        Task UpdateItemAsync(string id, Maquina item);
        Task DeleteItemAsync(string id);
    }

    public class CosmosDBServiceMaquina : ICosmosDBServiceMaquina
    {
        private Container _container;
        public CosmosDBServiceMaquina(CosmosClient dbClient, string dataBaseName, string containerName)
        {
            this._container = dbClient.GetContainer(dataBaseName, containerName);
        }

        public async Task<IEnumerable<Maquina>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Maquina>(new QueryDefinition(queryString));
            List<Maquina> results = new List<Maquina>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<Maquina> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Maquina> response = await this._container.ReadItemAsync<Maquina>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task AddItemAsync(Maquina item, string id)
        {
            await this._container.CreateItemAsync<Maquina>(item, new PartitionKey(id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Maquina>(id, new PartitionKey(id));
        }

        public async Task UpdateItemAsync(string id, Maquina item)
        {
            var prod = this.GetItemAsync(id).Result;

            prod.horas_en_reparacion = item.horas_en_reparacion;
            prod.cant_producto_x_hora = item.cant_producto_x_hora;
            prod.costo_operacion_x_hora = item.costo_operacion_x_hora;
            prod.estado = item.estado;

            await this._container.UpsertItemAsync<Maquina>(prod, new PartitionKey(id));
        }
    }

    public interface ICosmosDBServiceProducto
    {
        Task<IEnumerable<Producto>> GetItemsAsync(string query);
        Task<Producto> GetItemAsync(string id);
        Task AddItemAsync(Producto item, string id);
        Task UpdateItemAsync(string id, Producto item);
        Task DeleteItemAsync(string id);
    }

    public class CosmosDBServiceProducto : ICosmosDBServiceProducto
    {
        private Container _container;
        public CosmosDBServiceProducto(CosmosClient dbClient, string dataBaseName, string containerName)
        {
            this._container = dbClient.GetContainer(dataBaseName, containerName);
        }

        public async Task<IEnumerable<Producto>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Producto>(new QueryDefinition(queryString));
            List<Producto> results = new List<Producto>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<Producto> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Producto> response = await this._container.ReadItemAsync<Producto>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task AddItemAsync(Producto item, string id)
        {
            await this._container.CreateItemAsync<Producto>(item, new PartitionKey(id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Producto>(id, new PartitionKey(id));
        }

        public async Task UpdateItemAsync(string id, Producto item)
        {
            await this._container.UpsertItemAsync<Producto>(item, new PartitionKey(id));
        }
    }
}
