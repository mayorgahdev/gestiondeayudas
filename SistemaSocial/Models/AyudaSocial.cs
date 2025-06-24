using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class AyudaSocial
    {
        [Key]

        public int AyudaSocialID { get; set; }

        [Required]
        [Display(Name = "Nombre Usuario.")]
        public int ClientesID { get; set; }
        public Clientes Clientes { get; set; } // <- INYECCION DE DEPENDENCIA 

        [Required]
        [Display(Name = "Tipo Petición.")]
        public int TipoPeticionID { get; set; }
        public TipoPeticion TipoPeticion { get; set; } // <- INYECCION DE DEPENDENCIA 

        [Required]
        [Display(Name = "Prestación.")]
        public int PrestacionesID { get; set; } //(CARGAR PRESTACIONES)
        public Prestaciones Prestaciones { get; set; }

        [NotMapped]
        [Display(Name = "Prestación.")]
        public List<string> MultiPrestaciones { get; set; }

        [Required]
        [Display(Name = "Cantidad a Entregar.")]
        public int CantidadEntregada { get; set; }

        [Display(Name = "Stock en Bodega.")]
        public string StockBodega { get; set; }

        [Display(Name = "Detalle del Requerimiento.")]
        public string DetalleRequerimiento { get; set; }
        
        [Required]
        [Display(Name = "Fecha Elaboración.")]
        public DateTime FechaElaboracion { get; set; }

        [Display(Name = "N° Informe.")]
        public int NumInforme { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° de Ayuda:")]
        public int NumAyuda { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }

    }
}
