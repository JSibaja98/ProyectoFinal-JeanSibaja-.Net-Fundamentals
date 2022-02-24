using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_JeanSibaja.Models;
using ProyectoFinal_JeanSibaja.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_JeanSibaja.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ICosmosDBServiceProducto _cosmosService;

        public ProductoController(ICosmosDBServiceProducto cosmosDBService)
        {
            this._cosmosService = cosmosDBService;
        }

        public async Task<IActionResult> Producto()
        {
            return View((await this._cosmosService.GetItemsAsync("SELECT * FROM producto")).ToList());
        }


        public IActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> CreateProduct(Producto producto)
        {
            producto.id = Guid.NewGuid().ToString();

            await this._cosmosService.AddItemAsync(producto, producto.id);

            return RedirectToAction("Producto");

        }

        public IActionResult Edit(Producto prod)
        {
            return View(prod);
        }

        public async Task<ActionResult> EditProduct(Producto producto)
        {
            await this._cosmosService.UpdateItemAsync(producto.id,producto);

            return RedirectToAction("Producto");

        }

        public IActionResult Delete(Producto prod)
        {
            return View(prod);
        }

        public async Task<ActionResult> DeleteProduct(Producto producto)
        {
            await this._cosmosService.DeleteItemAsync(producto.id);

            return RedirectToAction("Producto");

        }
    }
}
