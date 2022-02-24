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
        private readonly ICosmosDBServiceMaquina _cosmosService;

        public MaquinaController(ICosmosDBServiceMaquina cosmosDBService)
        {
           this._cosmosService = cosmosDBService;
        }

        public async Task<IActionResult> Maquina()
        {
            return View((await this._cosmosService.GetItemsAsync("SELECT * FROM maquina")).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> CreateMaquina(Maquina maquina)
        {
            maquina.id = Guid.NewGuid().ToString();

            maquina.probabilidad_fallo = (new Random().Next(1,11)/10.00);

            await this._cosmosService.AddItemAsync(maquina, maquina.id);

            return RedirectToAction("Maquina");

        }

        public IActionResult Edit(Maquina maquina)
        {
            return View(maquina);
        }

        public async Task<ActionResult> EditMaquina(Maquina maquina)
        {
            await this._cosmosService.UpdateItemAsync(maquina.id, maquina);

            return RedirectToAction("Maquina");

        }

        public IActionResult Delete(Maquina maquina)
        {
            return View(maquina);
        }

        public async Task<ActionResult> DeleteProduct(Maquina maquina)
        {
            await this._cosmosService.DeleteItemAsync(maquina.id);

            return RedirectToAction("Maquina");

        }
    }
}
