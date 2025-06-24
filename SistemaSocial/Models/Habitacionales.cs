using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Habitacionales
    {
        [Key]
        public int HabitacionalesID { get; set; }

        [Display(Name = "Nombre Usuario.")]
        public int ClientesID { get; set; }
        public Clientes Clientes { get; set; } 

        [Display(Name = "Situación de la Vivienda.")]
        public int SituacionCasaID { get; set; }
        public SituacionCasa SituacionCasa { get; set; }


        //LUZ
        [Display(Name = "Red Pública.")]
        public string RedPublica { get; set; }

        [Display(Name = "Conexión Eléctrica Irregular.")]
        public string Luzirregular { get; set; }

        [Display(Name = "Sin Electricidad.")]
        public string SinLuz { get; set; }


        //AGUA
        [Display(Name = "Red Pública.")]
        public string Agua { get; set; }

        [Display(Name = "Agua por Camión Aljibe.")]
        public string AguaCamion { get; set; }

        [Display(Name = "Agua por Noria.")]
        public string AguaNoria { get; set; }

        [Display(Name = "Agua por Vertiente.")]
        public string AguaVertiente { get; set; }

        //Eliminación de Excretas
        [Display(Name = "Alcantarillado.")]
        public string Alcantarillado { get; set; }

        [Display(Name = "Fosa Séptica.")]
        public string Fosa { get; set; }

        [Display(Name = "Pozo Negro.")]
        public string Pozo { get; set; }

        [Display(Name = "Otro.")]
        public string Otro { get; set; }


        [Display(Name = "Material de la Vivienda.")]
        public int MaterialViviendaID { get; set; }
        public MaterialVivienda MaterialVivienda { get; set; }


        [Display(Name = "Defina el Material de la Vivienda.")]
        public string Definir { get; set; }


        [Display(Name = "Tenencia de la Vivienda.")]
        public int TipoViviendaID { get; set; }
        public TipoVivienda TipoVivienda { get; set; }


        [Display(Name = "Número de Dormitorios.")]
        public int NumDormitorios { get; set; }


        [Display(Name = "Total de Dependencias.")]
        public int TotalDependecias { get; set; }


        [Display(Name = "Hacinamiento.")]
        public string NivelAsinamiento { get; set; }


        [Display(Name = "Tenencia del Terreno.")]
        public int TenenciaTerrenoID { get; set; } 
        public TenenciaTerreno TenenciaTerreno { get; set; }


        [Display(Name = "Observaciones.")]
        public string Observaciones { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }
    }
}
