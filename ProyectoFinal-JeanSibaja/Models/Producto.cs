using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_JeanSibaja.Models
{
    public class Producto
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName ="nombre")]
        public string nombre { get; set; }

        [JsonProperty(PropertyName = "precio")]
        public double precio { get; set; }
    }
}
