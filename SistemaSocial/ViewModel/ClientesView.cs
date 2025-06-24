using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaSocial.Models;
using System.Collections.Generic;

namespace SistemaSocial.ViewModel
{
    public class ClientesView
    {
        public List<Clientes> listaClientes { get; set; }

        //BUSQUEDA
        public SelectList Nombre { get; set; }
        public string RutCliente { get; set; }
        public string Busqueda { get; set; }

        //PAGINADO
        public int Paginas { get; set; }
        public int CantidadPaginas { get; set; }
    }
}
