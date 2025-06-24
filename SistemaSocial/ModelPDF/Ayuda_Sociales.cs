using SistemaSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.ModelPDF
{
    public class Ayuda_Sociales
    {
        public List<Clientes> ListaClientes { get; set; }
        public List<AyudaSocial> ListadoAyudas { get; set; }
    }
}
