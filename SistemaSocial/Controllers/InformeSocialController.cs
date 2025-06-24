using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using SistemaSocial.Models;
using SistemaSocial.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Controllers
{
    [Authorize(Policy = "PolicyAdmin")]
    public class InformeSocialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;


        public InformeSocialController(
            Microsoft.AspNetCore.Identity.UserManager<Usuario> userManager, 
            SignInManager<Usuario> signInManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        private void Datos()
        {
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Rut");
            ViewData["AyudaSocialID"] = new SelectList(_context.tblAyudaSocial.ToList(), "AyudaSocialID", "PrestacionID");
            ViewData["AsignacionDeCuentaID"] = new SelectList(_context.tblAsignacion.ToList(), "AsignacionDeCuentaID", "CuentaID");
            ViewData["MedioID"] = new SelectList(_context.tblMedioAyuda.ToList(), "MedioID", "MedioID");
            ViewData["PrestacionesID"] = new SelectList(_context.tblPrestaciones.ToList(), "PrestacionesID", "Nombre");
            ViewData["ProgramaID"] = new SelectList(_context.tblPrograma.ToList(), "ProgramaID", "Nombre");
            ViewData["CuentaID"] = new SelectList(_context.tblCuenta.ToList(), "CuentaID", "Codigo");

            ViewData["Id"] = new SelectList(_context.Users.ToList(), "Id", "UserName");
        }
        private void DatosInforme()
        {
            //CLIENTE
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Rut");
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



        //[AllowAnonymous]
        public IActionResult IndexInformeSocial(int Pagina, string Busqueda)
        {
            Datos();
            var ivm = new InformeSocialView();

            if (Pagina == 0) {
                ivm.Paginas = 1;
            }
            else {
                ivm.Paginas = Pagina;
            }

            int Muestra = 15;
            int Cantidad = _context.tblInformeSocial.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                ivm.CantidadPaginas = Cantidad;
            }
            else {
                ivm.CantidadPaginas = Cantidad + 1;
            }

            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;


            //BUSQUEDA
            var informe = from c in _context.tblInformeSocial select c;

            if (!String.IsNullOrEmpty(Busqueda)) {
                informe = informe.Where(c =>
                c.NumInforme.ToString().Contains(Busqueda)
             || c.Clientes.Rut.Contains(Busqueda));
            }

            ivm.ListaInformeSocial = informe
                .Where(i => i.NumInforme == 0 || informe
                .Any(j => j.NumInforme == i.NumInforme && j.InformeSocialID > i.InformeSocialID) == false)
                .OrderByDescending(i => i.InformeSocialID)
                .Skip((ivm.Paginas - 1) * Muestra)
                .Take(Muestra)
                .ToList();

            return View(ivm);
        }
        public IActionResult Imprimir_Informe
            (int ClientesID, int InformeID, int NumInforme, int Grupo, int NumMedio, int NumCuenta, int NumAyuda)
        {
            DatosInforme();
            var informe = new InformeSocialView();

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
            return new ViewAsPdf("Imprimir_Informe", informe) /*{ FileName = "Informe Social (Completo).PDF" }*/;
        }



        //CREAR INFORME SOCIAL
        [HttpGet]
        public IActionResult CrearInformeSocial(int ClientesID, int NumCuenta, int Grupo, string Tipo, int NumInforme)
        {
            Datos();
            //AÑADIR OTRA ASIGNACION
            ViewBag.ClientesID1 = ClientesID;
            ViewBag.NumCuenta = NumCuenta;
            ViewBag.Grupo = Grupo;
            ViewBag.NumInforme = NumInforme;

            //VISTA INFORME
            ViewBag.Tipo = Tipo;
            ViewBag.AyudaSocialID = new SelectList(_context.tblAyudaSocial.OrderByDescending(x => x.AyudaSocialID), "AyudaSocialID", "Prestaciones.Nombre");
            ViewBag.AsignacionDeCuentaID = new SelectList(_context.tblAsignacion.OrderByDescending(x => x.AsignacionDeCuentaID), "AsignacionDeCuentaID", "Cuenta.Codigo");
            ViewBag.MedioID = new SelectList(_context.tblMedioAyuda.OrderByDescending(x => x.MedioID), "MedioID", "MedioID");
            ViewBag.UsuarioID = new SelectList(_context.Users.OrderBy(x => x.Id), "Id", "UserName").ToList();

            var ultimo = _context.tblClientes
                .Where(c => c.ClientesID.Equals(ClientesID))
                .OrderBy(x => x.ClientesID)
                .FirstOrDefault();

            ViewBag.ClientesID = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = ultimo.ClientesID.ToString(),
                    Text = $"{ ultimo.Nombres} {ultimo.ApellidoPaterno} {ultimo.ApellidoMaterno}"
                }
            }, "Value", "Text");

            InformeSocial informe = new InformeSocial();
            string fecha = DateTime.Now.ToString("dd-MM-yyyy");
            informe.FechaElaboracion = DateTime.ParseExact(fecha, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            return View(informe);
        }
        [HttpPost]
        public async Task<IActionResult> CrearInformeSocial(InformeSocial informeSocial, int ClientesID, int NumCuenta, int Grupo, string Tipo)
        {
            Datos();
            //AÑADIR OTRA ASIGNACION
            ViewBag.ClientesID1 = ClientesID;
            ViewBag.NumCuenta = NumCuenta;
            ViewBag.Informe = Grupo;

            //VISTA INFORME
            ViewBag.Tipo = Tipo;
            ViewBag.AyudaSocialID = new SelectList(_context.tblAyudaSocial.OrderByDescending(x => x.AyudaSocialID), "AyudaSocialID", "Prestaciones.Nombre");
            ViewBag.AsignacionDeCuentaID = new SelectList(_context.tblAsignacion.OrderByDescending(x => x.AsignacionDeCuentaID), "AsignacionDeCuentaID", "Cuenta.Codigo");
            ViewBag.MedioID = new SelectList(_context.tblMedioAyuda.OrderByDescending(x => x.MedioID), "MedioID", "MedioID");
            ViewBag.ClientesID = new SelectList(_context.tblInformeSocial.Where(x => x.Clientes.ClientesID.Equals(ClientesID))).FirstOrDefault();
            ViewBag.UsuarioID = new SelectList(_context.Users.OrderBy(x => x.Id), "Id", "UserName").ToList();

            var Clientes = _context.tblClientes.Select(x => new
            {
                id = x.ClientesID,
                nombre = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno

            }).OrderByDescending(x => x.id);
            ViewBag.ClientesID = new SelectList(Clientes, "id", "nombre");

            if (ModelState.IsValid) {
                _context.Add(informeSocial);
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexInformeSocial));
            }else {
                return View(informeSocial);
            }
        }



        //EDITAR INFORME SOCIAL
        [HttpGet]
        public IActionResult EditarInformeSocial(int InformeID, int ClientesID, int NumInforme)
        {
            Datos();
            var informeSocial = _context.tblInformeSocial
                .Where(S => S.InformeSocialID.Equals(InformeID))
                .Where(S => S.ClientesID.Equals(ClientesID))
                .Where(S => S.NumInforme.Equals(NumInforme))
                .FirstOrDefault();

            if (informeSocial != null) {
                var ultimo = _context.tblClientes.Select(x => new
                {
                    id = x.ClientesID,
                    CodigoYRazon = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno
                }).OrderBy(x => x.id);

                ViewBag.ClientesID = new SelectList(ultimo, "id", "CodigoYRazon");

                return View(informeSocial);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarInformeSocial(InformeSocial informeSocial)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblInformeSocial
                    .Where(S => S.InformeSocialID.Equals(informeSocial.InformeSocialID))
                    .Where(S => S.ClientesID.Equals(informeSocial.ClientesID))
                    .Where(S => S.NumInforme.Equals(informeSocial.NumInforme))
                    .FirstOrDefault();

                Editar.ClientesID = informeSocial.ClientesID;
                Editar.NumInforme = informeSocial.NumInforme;
                Editar.FechaElaboracion = informeSocial.FechaElaboracion;
                Editar.Sintesis = informeSocial.Sintesis;
                Editar.GrupoFamiliar = informeSocial.GrupoFamiliar;
                Editar.UsuarioID = informeSocial.UsuarioID;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");

            }else {
                return View();
            }
        }



        //ELIMINAR DATOS INFORME SOCIAL  
        [HttpGet]
        public IActionResult EliminarInformeSocial(int InformeSocialID)
        {
            Datos();
            ViewBag.AyudaSocialID = new SelectList(_context.tblAyudaSocial.OrderByDescending(x => x.AyudaSocialID), "AyudaSocialID", "Prestaciones.Nombre");
            ViewBag.AsignacionDeCuentaID = new SelectList(_context.tblAsignacion.OrderByDescending(x => x.AsignacionDeCuentaID), "AsignacionDeCuentaID", "Cuenta.Codigo");
            var informeSocial = _context.tblInformeSocial.Where(S => S.InformeSocialID.Equals(InformeSocialID)).FirstOrDefault();
            if (informeSocial != null) {
                return View(informeSocial);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarInformeSocial(InformeSocial informeSocial)
        {
            Datos();
            if (ModelState.IsValid) {
                var EliminarInformeSocial = _context.tblInformeSocial.Where(S => S.InformeSocialID.Equals(informeSocial.InformeSocialID)).FirstOrDefault();
                _context.Attach(EliminarInformeSocial).State = EntityState.Deleted;
                _context.SaveChanges();

                return RedirectToAction(nameof(IndexInformeSocial));

            }else {
                return View(informeSocial);
            }
        }
    }
}
