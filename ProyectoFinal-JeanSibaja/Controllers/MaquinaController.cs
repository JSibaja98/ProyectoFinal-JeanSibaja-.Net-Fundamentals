using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_JeanSibaja.Models;
using ProyectoFinal_JeanSibaja.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_JeanSibaja.Controllers
{
    public class MaquinaController : Controller
    {
        private readonly ICosmosDBService<Maquina> _cosmosService;

        public MaquinaController(ICosmosDBService<Maquina> cosmosDBService)
        {
            this._cosmosService = cosmosDBService;
        }

        public async Task<List<Maquina>> Index()
        {
            return (await this._cosmosService.GetItemsAsync("SELECT * FROM maquina")).ToList();
        }

        public async Task AddItem()
        {
            Maquina maquina = new Maquina();
            maquina.id = Guid.NewGuid().ToString();

            await this._cosmosService.AddItemAsync(maquina, maquina.id);
        }

        public async Task UpdateItem(string id)
        {
            Maquina maquina = new Maquina();
            maquina.id =id;

            await this._cosmosService.UpdateItemAsync(id, maquina);
        }

        public async Task DeleteItem(string id)
        {
            await this._cosmosService.DeleteItemAsync(id);
        }
    }
}
