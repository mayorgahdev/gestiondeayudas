using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.ViewModel
{
    public class AsignacionView
    {
        public List<AsignacionDeCuenta> listaAsignacion { get; set; }

        //BUSQUEDA
        public SelectList Nombre { get; set; }
        public string RutCliente { get; set; }
        public string Busqueda { get; set; }


        //PAGINADO
        public int Paginas { get; set; }
        public int CantidadPaginas { get; set; }
    }
}
