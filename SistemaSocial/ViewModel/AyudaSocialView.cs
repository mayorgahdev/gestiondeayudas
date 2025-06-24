using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.ViewModel
{
    public class AyudaSocialView
    {
        public List<AyudaSocial> listaAyudas { get; set; }
        public List<Clientes> listaClientes { get; set; }
        public List<Usuario> ListaUsuarios { get; set; }


        //BUSQUEDA
        public SelectList Nombre { get; set; }
        public string RutCliente { get; set; }
        public string Busqueda { get; set; }



        //BUSQUEDA POR FECHAS
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }



        //Paginado
        public int Paginas { get; set; }
        public int CantidadPaginas { get; set; }
        public int Cantidad { get; set; }

    }
}