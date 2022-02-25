using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_JeanSibaja.Models
{
    public class Simulacion
    {
        public Simulacion()
        {
            Maquina1 = "";
            Maquina2 = "";
            Producto = "";
            Maquina_Recomendada = "";
            id = "";
        }
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        /*  Datos de la produccion diaria  */

        [JsonProperty(PropertyName = "Cantidad_dias_produccion_semanal")]
        public int Cantidad_dias_produccion_semanal { get; set; }
        [JsonProperty(PropertyName = "Cantidad_horas_produccion_diaria")]
        public int Cantidad_horas_produccion_diaria { get; set; }
        
        /*  Datos de la duracion de la simulacion   */

        [JsonProperty(PropertyName = "Cantidad_Meses_Simulacion")]
        public int Cantidad_Meses_Simulacion { get; set; }
        [JsonProperty(PropertyName = "Cantidad_Dias_Simulacion")]
        public int Cantidad_Dias_Simulacion { get; set; }
        [JsonProperty(PropertyName = "Cantidad_Horas_Simulacion ")]
        public int Cantidad_Horas_Simulacion { get; set; }

        /*  Datos de las maquinas para la simulacion  */

        [JsonProperty(PropertyName = "Precio_x_fabricacion_del_Producto")]
        public int Precio_x_fabricacion_del_Producto { get; set; }
        [JsonProperty(PropertyName = "Maquina1")]
        public string Maquina1 { get; set; }
        [JsonProperty(PropertyName = "Maquina2")]
        public string Maquina2 { get; set; }
        [JsonProperty(PropertyName = "Producto")]
        public string Producto { get; set; }

        /*  Datos de resultado  */

        [JsonProperty(PropertyName = "Productos_Construidos_M1")]
        public int Productos_Construidos_M1 { get; set; }
        [JsonProperty(PropertyName = "Productos_Construidos_M2")]
        public int Productos_Construidos_M2 { get; set; }

        [JsonProperty(PropertyName = "Ganancia_Productos_M1")]
        public int Ganancia_Productos_M1 { get; set; }
        [JsonProperty(PropertyName = "Ganancia_Productos_M2")]
        public int Ganancia_Productos_M2 { get; set; }

        [JsonProperty(PropertyName = "Ganancia_Real_M1")]
        public int Ganancia_Real_M1 { get; set; }
        [JsonProperty(PropertyName = "Ganancia_Real_M2")]
        public int Ganancia_Real_M2 { get; set; }

        [JsonProperty(PropertyName = "Maquina_Recomendada")]
        public string Maquina_Recomendada { get; set; }

    }
}
