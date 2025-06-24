using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using SistemaSocial.ModelPDF;
using SistemaSocial.Models;
using SistemaSocial.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Controllers
{
    public class CrearPDFController : Controller
    {
        private readonly AppDbContext _context;
        public CrearPDFController(AppDbContext context)
        {
            _context = context;
        }

        private void DatosInforme()
        {
            //CLIENTE
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Rut");
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Pasaporte");
            ViewData["NacionalidadID"] = new SelectList(_context.tblNacionalidad.ToList(), "NacionalidadID", "Nombre");
            ViewData["PrevisionSocialID"] = new SelectList(_context.tblPrevisionSocial.ToList(), "PrevisionSocialID", "Nombre");
            ViewData["PrevisionSaludID"] = new SelectList(_context.tblPrevisionSalud.ToList(), "PrevisionSaludID", "Nombre");
            ViewData["IndiceEscolaridadID"] = new SelectList(_context.tblIndiceEscolaridad.ToList(), "IndiceEscolaridadID", "Nombre");
            ViewData["ParentescoID"] = new SelectList(_context.tblParentesco.ToList(), "ParentescoID", "Nombre");
            ViewData["EstadoCivilID"] = new SelectList(_context.tblEstadoCivil.ToList(), "EstadoCivilID", "Nombre");

            //SALUD
            ViewData["ProcedenciaID"] = new SelectList(_context.tblProcedencia.ToList(), "ProcedenciaID", "Nombre");
            ViewData["DiscapacidadID"] = new SelectList(_context.tblDiscapacidad.ToList(), "DiscapacidadID", "Nombre");
            ViewData["AcreditacionID"] = new SelectList(_context.tblAcreditacion.ToList(), "AcreditacionID", "Nombre");

            //SOCIOECONOMICO
            ViewData["TipoPensionID"] = new SelectList(_context.tblTipoPension.ToList(), "TipoPensionID", "Nombre");
            ViewData["TipoSubsidioID"] = new SelectList(_context.tblTipoSubsidio.ToList(), "TipoSubsidioID", "Nombre");
            ViewData["RSHID"] = new SelectList(_context.tblRSH.ToList(), "RSHID", "Nombre");
            ViewData["TramoID"] = new SelectList(_context.tblTramo.ToList(), "TramoID", "Nombre");

            //HABITACIONALES
            ViewData["SituacionCasaID"] = new SelectList(_context.tblSituacionCasa.ToList(), "SituacionCasaID", "Nombre");
            ViewData["MaterialViviendaID"] = new SelectList(_context.tblMaterialVivienda.ToList(), "MaterialViviendaID", "Nombre");
            ViewData["TipoViviendaID"] = new SelectList(_context.tblTipoVivienda.ToList(), "TipoViviendaID", "Nombre");
            ViewData["TenenciaTerrenoID"] = new SelectList(_context.tblTenenciaTerreno.ToList(), "TenenciaTerrenoID", "Nombre");

            //AYUDA SOCIAL
            ViewData["PrestacionesID"] = new SelectList(_context.tblPrestaciones.ToList(), "PrestacionesID", "Nombre");
            ViewData["TipoPeticionID"] = new SelectList(_context.tblTipoPeticion.ToList(), "TipoPeticionID", "Nombre");

            //ASIGNACION DE CUENTA
            ViewData["AreaGestionID"] = new SelectList(_context.tblAreaGestion.ToList(), "AreaGestionID", "Nombre");
            ViewData["ProgramaID"] = new SelectList(_context.tblPrograma.ToList(), "ProgramaID", "Nombre");
            ViewData["CuentaID"] = new SelectList(_context.tblCuenta.ToList(), "CuentaID", "Codigo");
            ViewData["PresupuestoID"] = new SelectList(_context.tblPresupuesto.ToList(), "PresupuestoID", "PresInicial");

            //PROFESIONALES
            ViewData["TipoUsuarioID"] = new SelectList(_context.tblTipoUsuario.ToList(), "TipoUsuarioID", "Nombre");
            ViewData["ProfesionID"] = new SelectList(_context.tblProfesion.ToList(), "ProfesionID", "Nombre");
        }

        public IActionResult Informe_Socialx2
            (int ClientesID, int InformeID, int NumInforme, int Grupo, int NumMedio, int NumCuenta, int NumAyuda)
        {
            DatosInforme();
            if (ModelState.IsValid) {
                DatosInforme();

                var informe = new Informe();

                var query1 = from cliente in _context.tblClientes select cliente;
                var query2 = from grupo in _context.tblClientes select grupo;
                var query3 = from cronico in _context.tblCronicos select cronico;
                var query4 = from salud in _context.tblSalud select salud;
                var query5 = from econimico in _context.tblSocioeconomico select econimico;
                var query6 = from habitacional in _context.tblHabitacionales select habitacional;
                var query7 = from situacion in _context.tblSituacionActual select situacion;
                var query8 = from gastos in _context.tblGastosFamiliares select gastos;
                var query9 = from ayudas in _context.tblAyudaSocial select ayudas;
                var query10 = from peticion in _context.tblPeticionAyuda select peticion;
                var query11 = from medio in _context.tblMedioAyuda select medio;
                var query12 = from informes in _context.tblInformeSocial select informes;
                var query13 = from asignacion in _context.tblAsignacion select asignacion;
                var query14 = from usuario in _context.Users select usuario;


                // USUARIOS Y GRUPO FAMILIAR
                informe.ListaClientes = query1
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .ToList();

                informe.ListaClientesGrupo = query1
                    .Where(C => C.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(x.ClientesID))
                    .OrderBy(x => x.ClientesID)
                    .ToList();

                informe.ListaGrupo = query2
                    .Where(C => C.GrupoFamiliar.Equals(Grupo))
                    .OrderByDescending(C => C.TipoDeCliente.Equals("Requirente"))
                    .ToList();


                //CRONICOS GRUPO FAMILIAR
                informe.ListaCronicos = query3
                    .Where(x => x.GrupoFamiliar.Equals(Grupo))
                    .ToList();


                // SALUD Y GRUPO FAMILIAR
                informe.ListaSalud = query4
                    .Include("Clientes")
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .ToList();

                informe.ListaSaludGrupo = query4
                    .Include("Clientes")
                    .Where(C => C.Clientes.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(x.ClientesID))
                    .OrderBy(x => x.ClientesID)
                    .ToList();


                //SOCIOECONOMICOS Y GRUPO FAMILIAR
                informe.ListaSocioeconomico = query5
                    .Include("Clientes")
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .ToList();

                informe.ListaSocioeconomicoGrupo = query5
                    .Include("Clientes")
                    .Where(C => C.Clientes.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(x.ClientesID))
                    .OrderBy(x => x.ClientesID)
                    .ToList();


                // HABITACIONALES Y GRUPO FAMILIAR
                informe.ListaHabitacionales = query6
                    .Include("Clientes")
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .ToList();

                informe.ListaHabitacionalesGrupo = query6
                    .Include("Clientes")
                    .Where(C => C.Clientes.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(x.ClientesID))
                    .OrderBy(x => x.ClientesID)
                    .ToList();


                // SITUACION ACTUAL Y GRUPO FAMILIAR
                informe.listaSituacion = query7
                    .Include("Clientes")
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .ToList();

                informe.listaSituacionGrupo = query7
                    .Include("Clientes")
                    .Where(C => C.Clientes.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(x.ClientesID))
                    .OrderBy(x => x.ClientesID)
                    .ToList();


                // GASTOS FAMILIARES Y GRUPO FAMILIAR
                informe.ListaGastosFamiliares = query8
                    .Include("Clientes")
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .ToList();

                informe.ListaGastosFamiliaresGrupo = query8
                    .Include("Clientes")
                    .Where(C => C.Clientes.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(x.ClientesID))
                    .OrderBy(x => x.ClientesID)
                    .ToList();


                // AYUDAS SOCIALES
                informe.ListaAyudaSocial = query9
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .Where(x => x.NumAyuda.Equals(NumAyuda))
                    .OrderBy(x => x.AyudaSocialID)
                    .ToList();

                informe.ListaAyudaSocialGrupo = query9
                    .Include("Clientes")
                    .Where(C => C.Clientes.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(x.ClientesID))
                    .OrderBy(x => x.ClientesID)
                    .ToList();


                //PETICION AYUDA
                informe.ListPeticionAyudas = query10
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .Where(x => x.NumInforme.Equals(NumInforme))
                    .OrderBy(x => x.ClientesID)
                    .ToList();

                informe.ListPeticionAyudasGrupo = query10
                    .Include("Clientes")
                    .Where(C => C.Clientes.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .Where(x => x.NumInforme.Equals(NumInforme))
                    .OrderByDescending(x => x.ClientesID)
                    .ToList();

                // MEDIO DE HACER EFECTIVA LA AYUDA SOCIAL
                informe.ListaMedio = query11
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .Where(x => x.NumMedio.Equals(NumMedio))
                    .OrderBy(x => x.MedioID)
                    .ToList();

                informe.ListaMedioGrupo = query11
                    .Include("Clientes")
                    .Where(C => C.Clientes.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(x.ClientesID))
                    .OrderBy(x => x.ClientesID)
                    .ToList();


                //INFORME SOCIAL
                informe.ListaInformeSocial = query12
                    .Include("Usuario")
                    .Where(x => x.InformeSocialID.Equals(InformeID))
                    .Where(x => x.NumInforme.Equals(NumInforme))
                    .ToList();

                // ASIGNACION DE CUENTAS
                informe.ListaAsignacion = query13
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .Where(x => x.NumCuenta.Equals(NumCuenta))
                    .OrderBy(x => x.AsignacionDeCuentaID)
                    .ToList();

                informe.ListaAsignacionGrupo = query13
                    .Include("Clientes")
                    .Where(C => C.Clientes.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(x.ClientesID))
                    .OrderBy(x => x.ClientesID)
                    .ToList();

                { informe.ID = ClientesID; }
                //return View(informe);
                return new ViewAsPdf("Informe_Socialx2", informe) /*{ FileName = "Informe Social (Reducido).PDF" }*/ ;
            }
            else {
                return NotFound();
            }
        }
       
        public IActionResult ImprimirAyudaSociales
            (int ClientesID, int Grupo, string User)
        {
            if (ModelState.IsValid) {
                DatosInforme();
                var informe = new Informe();

                var query1 = from cliente in _context.tblClientes select cliente;
                var query2 = from ayudas in _context.tblAyudaSocial select ayudas;
                var query3 = from usuario in _context.Users select usuario;
                var query4 = from peticion in _context.tblPeticionAyuda select peticion;
                var query5 = from informes in _context.tblInformeSocial select informes;

                informe.ListaClientes = query1
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .ToList();

                informe.ListaAyudaSocial = query2
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .ToList();

                //PETICION AYUDA
                informe.ListPeticionAyudas = query4
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .OrderBy(x => x.ClientesID)
                    .ToList();

                informe.ListPeticionAyudasGrupo = query4
                    .Include("Clientes")
                    .Where(C => C.Clientes.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .OrderByDescending(x => x.ClientesID)
                    .ToList();

                informe.ListaUsuarios = query3
                    .Where(x => x.UserName.Equals(User))
                    .ToList();

                informe.ListaInformeSocial = query5
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .OrderByDescending(x => x.InformeSocialID)
                    .ToList();

                int total = _context.tblAyudaSocial.Where(x => x.ClientesID.Equals(ClientesID)).ToList().Count;
                informe.Cantidad = total;

                //return View(informe);
                return new ViewAsPdf("ImprimirAyudaSociales", informe);

            }else {
                return NotFound();
            }
        }
       
        public IActionResult ImprimirGastos
            (int ClientesID, int GrupoFamiliar, string User, int Grupo)
        {
            DatosInforme();
            if (ModelState.IsValid) {
                var informe = new Informe();
                var query1 = from c in _context.tblClientes select c;
                var query2 = from grupo in _context.tblClientes select grupo;
                var query4 = from g in _context.tblGastosFamiliares select g;
                var query5 = from econimico in _context.tblSocioeconomico select econimico;
                var query6 = from usuario in _context.Users select usuario;
                var query13 = from ingresos in _context.tblIngresos select ingresos;

                informe.ListaIngresos = query13
                    .Where(C => C.GrupoFamiliar.Equals(Grupo))
                    .Where(x => x.ClientesID.Equals(x.ClientesID))
                    .OrderBy(x => x.ClientesID)
                    .ToList();

                informe.ListaClientes = query1
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .ToList();

                informe.ListaClientesGrupo = query1
                   .Where(C => C.GrupoFamiliar.Equals(Grupo))
                   .Where(x => x.ClientesID.Equals(x.ClientesID))
                   .OrderBy(x => x.ClientesID)
                   .ToList();

                informe.ListaGrupo = query2
                    .Where(C => C.GrupoFamiliar.Equals(GrupoFamiliar))
                    .ToList();

                informe.ListaGastosFamiliares = query4
                   .Where(C => C.GrupoFamiliar.Equals(Grupo))
                   .Where(x => x.ClientesID.Equals(x.ClientesID))
                   .OrderBy(x => x.ClientesID)
                   .ToList();

                informe.ListaSocioeconomico = query5.Include("Clientes")
                    .Where(x => x.ClientesID.Equals(ClientesID))
                    .ToList();

                informe.ListaUsuarios = query6.Where(x => x.UserName.Equals(User)).ToList();

                //return View(informe);
                return new ViewAsPdf("ImprimirGastos", informe);

            }else {
                return NotFound();
            }
        }
    }
}
