using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class GastosFamiliares
    {
        [Key]
        public int GastosFamiliaresID { get; set; }

        [Display(Name = "Nombre Usuario.")]
        public int ClientesID { get; set; }
        public Clientes Clientes { get; set; }

        [Display(Name = "Alimentación.")]
        public int Alimentacion { get; set; }

        [Display(Name = "Útiles de Aseo.")]
        public int Aseo { get; set; }

        [Display(Name = "Arriendo.")]
        public int Arriendo { get; set; }

        [Display(Name = "Dividendo.")]
        public int Dividendo { get; set; }

        [Display(Name = "Servicios Básicos (Luz, Agua).")]
        public int Servicios { get; set; }

        [Display(Name = "Combustible (Gas, Parafina, Leña, Bencina).")]
        public int Combustible { get; set; }

        [Display(Name = "Gastos Telefónicos (Telefonía, Internet, Cable).")]
        public int GastosTelefono { get; set; }

        [Display(Name = "Movilización (Transporte Público o Privado).")]
        public int Movilizacion { get; set; }

        [Display(Name = "Educación.")]
        public int Educacion { get; set; }

        [Display(Name = "Créditos (Formal, Informal).")]
        public int Creditos { get; set; }

        [Display(Name = "Salud.")]
        public int Salud { get; set; }

        [Display(Name = "Otros.")]
        public int Varios { get; set; }

        [Display(Name = "Total Gastos Mensuales: ")]
        public int TotalGastosMensuales { get; set; }

        [Display(Name = "Observaciones.")]
        public string Observaciones { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }
    }
}
