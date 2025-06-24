using SistemaSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.ViewModel
{
    public class UsuarioView
    {
        public List<Usuario> listaUsuario { get; set; }

        //PAGINADO
        public int Paginas { get; set; }
        public int CantidadPaginas { get; set; }
    }
}
