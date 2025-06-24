using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class AsignacionDeCuenta
    {
        [Key]
        public int AsignacionDeCuentaID { get; set; }


        [Display(Name = "Nombre Usuario.")]
        public int ClientesID { get; set; }
        public Clientes Clientes { get; set; }


        /* AREAS DE GESTION */
        [Display(Name = "Área de Gestión.")]
        public int AreaGestionID { get; set; }
        public AreaGestion AreaGestion { get; set; }

        /* PROGRAMAS */
        [Display(Name = "Programa.")]
        public int ProgramaID { get; set; }
        public Programa Programa { get; set; }

        /* CUENTAS */
        [Display(Name = "Cuenta.")]
        public int CuentaID { get; set; }
        public Cuenta Cuenta { get; set; }

        /* PRESUPUESTOS  */
        [Display(Name = "Presupuesto.")]
        public int PresupuestoID { get; set; }
        public Presupuesto Presupuesto { get; set; }

        /* PRESUPUESTOS ENTREGADOS */
        [Display(Name = "Presupuesto a Entregar.")]
        public int PresEntregado { get; set; }

        [Display(Name = "N° Informe.")]
        public int NumInforme { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° de Cuenta.")]
        public int NumCuenta { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }

    }
}
