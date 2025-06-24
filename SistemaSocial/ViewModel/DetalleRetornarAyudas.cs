using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.ViewModel
{
    public class DetalleRetornarAyudas
    {
        public string fecha { get; set; }
        public int totalAyudas { get; set; }


        //BUSQUEDA POR FECHAS
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
