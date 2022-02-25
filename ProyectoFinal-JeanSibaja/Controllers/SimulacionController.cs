using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_JeanSibaja.Models;
using ProyectoFinal_JeanSibaja.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_JeanSibaja.Controllers
{
    public class SimulacionController : Controller
    {
        private readonly ICosmosDBServiceMaquina _cosmosServiceMaquina;
        private readonly ICosmosDBServiceProducto _cosmosServiceProducto;
        private readonly ICosmosDBServiceSimulacion _cosmosServiceSimulacion;

        public SimulacionController(ICosmosDBServiceMaquina cosmosDBServiceMaquina, ICosmosDBServiceSimulacion cosmosDBServiceSimulacion, ICosmosDBServiceProducto cosmosDBServiceProducto)
        {
            this._cosmosServiceMaquina = cosmosDBServiceMaquina;
            this._cosmosServiceSimulacion = cosmosDBServiceSimulacion;
            this._cosmosServiceProducto = cosmosDBServiceProducto;
        }

        // GET: Simulacion
        public ActionResult Details()
        {
            IEnumerable<Simulacion> simulaciones = this._cosmosServiceSimulacion.GetItemsAsync("SELECT * FROM simulaciones").Result;
            var simulacionResult = simulaciones.ToList();
            return View(simulacionResult[(simulaciones.ToList().Count()-1)]);
        }

        // GET: Lista de Maquinas
        public IActionResult Create()
        {
            IEnumerable<Maquina> lista_maquinas = this._cosmosServiceMaquina.GetItemsAsync("SELECT * FROM maquina").Result;
            ModelosSimulacion model = new ModelosSimulacion();
            model.Maquinas = lista_maquinas;

            IEnumerable<Producto> lista_productos = this._cosmosServiceProducto.GetItemsAsync("SELECT * FROM producto").Result;
            model.Productos = lista_productos;

            return View(model);
        }

        public async Task<ActionResult> CreateSimulacion(ModelosSimulacion modelos)
        {
            modelos.Simulacion.id = Guid.NewGuid().ToString();

            #region Calculo de dias efectivos

            int totalDiasSemanalesEfectivos = (modelos.Simulacion.Cantidad_dias_produccion_semanal * 4) * (modelos.Simulacion.Cantidad_Meses_Simulacion); //20 dias 

            int contador_dias = 1;
            int resta_dias = 7 - modelos.Simulacion.Cantidad_dias_produccion_semanal;

            for (int j = 1; j <= modelos.Simulacion.Cantidad_Dias_Simulacion; j++)
            {
                if (contador_dias == modelos.Simulacion.Cantidad_dias_produccion_semanal)
                {
                    totalDiasSemanalesEfectivos++;
                    j += resta_dias;
                    contador_dias = 1;
                }
                else
                {
                    if (contador_dias <= modelos.Simulacion.Cantidad_dias_produccion_semanal)
                    {
                        totalDiasSemanalesEfectivos++;
                        contador_dias++;
                    }
                }
            }

            #endregion


            #region Calculo de horas Efectivas

            int totalHorasDiariasEfectivas = (modelos.Simulacion.Cantidad_horas_produccion_diaria * totalDiasSemanalesEfectivos);


            int contador_horas = 1;
            int resta_horas = 24 - modelos.Simulacion.Cantidad_horas_produccion_diaria; 

            for (int j = 1; j <= modelos.Simulacion.Cantidad_Horas_Simulacion; j++) 
            {
                if (contador_horas == modelos.Simulacion.Cantidad_horas_produccion_diaria)
                {
                    totalHorasDiariasEfectivas++; 
                    j += resta_horas;
                    contador_horas = 1;
                }
                else
                {
                    if (contador_horas <= modelos.Simulacion.Cantidad_horas_produccion_diaria)
                    {
                        totalHorasDiariasEfectivas++;
                        contador_horas++;
                    }
                }
            }

            #endregion

            #region Calculos de Simulacion

            Maquina ma1 = this._cosmosServiceMaquina.GetItemAsync(modelos.Simulacion.Maquina1).Result;
            Maquina ma2 = this._cosmosServiceMaquina.GetItemAsync(modelos.Simulacion.Maquina2).Result;

            int contadorMa1 = 1;
            int contadorMa2 = 1;

            #region  Cantidad de productos por maquina

            for (int i = 1; i <= totalHorasDiariasEfectivas; i++)
            {
                if(contadorMa1 <= modelos.Simulacion.Cantidad_horas_produccion_diaria) //maquina 1
                {
                    modelos.Simulacion.Productos_Construidos_M1 += ma1.cant_producto_x_hora;
                    contadorMa1 = 1;
                }

                if (contadorMa2 <= modelos.Simulacion.Cantidad_horas_produccion_diaria) //maquina 2
                {
                    modelos.Simulacion.Productos_Construidos_M2 +=ma2.cant_producto_x_hora;
                    contadorMa2 = 1;
                }

                contadorMa1++;
                contadorMa2++;
            }
            #endregion

            Producto producto = this._cosmosServiceProducto.GetItemAsync(modelos.Simulacion.Producto).Result;

            #region Ganancia bruta

            modelos.Simulacion.Ganancia_Productos_M1 = (modelos.Simulacion.Productos_Construidos_M1 * producto.precio);
            modelos.Simulacion.Ganancia_Productos_M2 = (modelos.Simulacion.Productos_Construidos_M2 * producto.precio);

            #endregion

            #region Ganancia neta

            modelos.Simulacion.Ganancia_Real_M1 = (modelos.Simulacion.Ganancia_Productos_M1 - (modelos.Simulacion.Productos_Construidos_M1 * modelos.Simulacion.Precio_x_fabricacion_del_Producto));
            modelos.Simulacion.Ganancia_Real_M2 = (modelos.Simulacion.Ganancia_Productos_M2 - (modelos.Simulacion.Productos_Construidos_M2 * modelos.Simulacion.Precio_x_fabricacion_del_Producto));

            #endregion

            modelos.Simulacion.Maquina_Recomendada = modelos.Simulacion.Ganancia_Real_M1 > modelos.Simulacion.Ganancia_Real_M2 ? ma1.id : ma2.id;

            #endregion

            await this._cosmosServiceSimulacion.AddItemAsync(modelos.Simulacion, modelos.Simulacion.id);

            return RedirectToAction("Details");

        }

    }
}
