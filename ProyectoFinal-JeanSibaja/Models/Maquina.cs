using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_JeanSibaja.Models
{
    public class Maquina
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "cant_producto_x_hora")]
        public int cant_producto_x_hora { get; set; }

        [JsonProperty(PropertyName = "costo_operacion_x_hora")]
        public double costo_operacion_x_hora { get; set; }

        [JsonProperty(PropertyName = "probabilidad_fallo")]
        public double probabilidad_fallo { get; set; }

        [JsonProperty(PropertyName = "horas_en_reparacion")]
        public double horas_en_reparacion { get; set; }

        [JsonProperty(PropertyName = "fecha_de_compra")]
        public DateTime fecha_de_compra { get; set; }

        [JsonProperty(PropertyName = "estado")]
        public Boolean estado { get; set; }
    }
}
