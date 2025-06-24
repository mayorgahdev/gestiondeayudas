using System;
using System.Collections.Generic;
using SistemaSocial.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaSocial.ViewModel
{
    public class Pension_Y_Subsidio
    {
        public List<TipoPension> ListPension { get; set; }
        public List<TipoSubsidio> ListSubsidio { get; set; }


        //BUSQUEDA
        public SelectList Nombre { get; set; }
        public string RutCliente { get; set; }
        public string Busqueda { get; set; }


        //PAGINADO
        public int Paginas { get; set; }
        public int CantidadPaginas { get; set; }
    }
}
