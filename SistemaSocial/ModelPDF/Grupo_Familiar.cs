using SistemaSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.ModelPDF
{
    public class Grupo_Familiar
    {
        public List<Clientes> listaClientes { get; set; }
        public List<InformeSocial> ListaInformeSocial { get; set; }
    }
}
