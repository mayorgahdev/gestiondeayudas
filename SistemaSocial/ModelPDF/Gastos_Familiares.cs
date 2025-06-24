using SistemaSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.ModelPDF
{
    public class Gastos_Familiares
    {
        public List<Clientes> ListaClientes { get; set; }
        public List<GastosFamiliares> ListadoGastos { get; set; }
        public List<InformeSocial> ListaInformeSocial { get; set; }
    }
}
