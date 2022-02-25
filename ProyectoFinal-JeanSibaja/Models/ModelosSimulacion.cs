using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_JeanSibaja.Models
{
    public class ModelosSimulacion
    {
        public Simulacion Simulacion { get; set; }

        public IEnumerable<Maquina> Maquinas { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
    }
}
