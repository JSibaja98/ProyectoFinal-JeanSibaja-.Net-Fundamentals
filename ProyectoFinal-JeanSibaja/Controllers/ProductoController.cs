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
        private readonly ICosmosDBService<Producto> _cosmosService;

        public ProductoController(ICosmosDBService<Producto> cosmosDBService)
        {
            this._cosmosService = cosmosDBService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
