using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaSocial.Models;
using SistemaSocial.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Controllers
{
    [Authorize(Policy = "PolicyAdmin")]
    public class AsignacionDeCuentasController : Controller
    {
        private readonly AppDbContext _context;

        public AsignacionDeCuentasController(AppDbContext context) {
            _context = context;
        }

        private void Datos() 
        {
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Rut");
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Pasaporte");
            ViewData["AreaGestionID"] = new SelectList(_context.tblAreaGestion.ToList(), "AreaGestionID", "Nombre");
            ViewData["ProgramaID"] = new SelectList(_context.tblPrograma.ToList(), "ProgramaID", "Nombre");
            ViewData["CuentaID"] = new SelectList(_context.tblCuenta.ToList(), "CuentaID", "Codigo");
            ViewData["PresupuestoID"] = new SelectList(_context.tblPresupuesto.ToList(), "PresupuestoID", "PresVigente");
        }



        public IActionResult IndexAsignacion(int Pagina, string Busqueda)
        {
            Datos();
            var avm = new AsignacionView();

            //PAGINADO
            if (Pagina == 0) {
                avm.Paginas = 1;
            }else {
                avm.Paginas = Pagina;
            }
            int Muestra = 15;
            int Cantidad = _context.tblAsignacion.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                avm.CantidadPaginas = Cantidad;
            }else {
                avm.CantidadPaginas = Cantidad + 1;
            }
            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;


            //BUSQUEDA
            var asignacion = from c in _context.tblAsignacion select c;
            if (!String.IsNullOrEmpty(Busqueda)) {
                asignacion = asignacion.Where(c =>
                  c.Clientes.Rut.Contains(Busqueda)
               || c.AreaGestion.Nombre.Contains(Busqueda)
               || c.Programa.Nombre.Contains(Busqueda)
               || c.Cuenta.Codigo.Contains(Busqueda));
            }

            avm.listaAsignacion = asignacion
                .OrderByDescending(c => c.ClientesID)
                .Skip((avm.Paginas - 1) * Muestra)
                .Take(Muestra)
                .ToList();

            return View(avm);
        }
        public IActionResult IndexPresupuesto()
        {
            Datos();
            var presupuesto = _context.tblPresupuesto.ToList();
            return View(presupuesto);
        }



        //CREAR ASIGNACION DE CUENTA
        [HttpGet]
        public IActionResult CrearAsignacion(int ClientesID, int NumMedio, int Grupo, string Tipo, int NumInforme)
        {
            Datos();
            ViewBag.ClientesID1 = ClientesID;
            ViewBag.NumMedio = NumMedio;
            ViewBag.Grupo = Grupo;
            ViewBag.Tipo = Tipo;
            ViewBag.NumInforme = NumInforme;

            ViewBag.PresEntregado = (from C in _context.tblMedioAyuda where C.ClientesID == ClientesID select C).OrderByDescending(x => x.ClientesID).First().Monto;

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

            return View();
        }
            [HttpGet]
            public async Task<ActionResult> ProgramasLoad()
            {
                var Programa = _context.tblPrograma.ToList();
                return Json(Programa);
            }
            [HttpGet]
            public async Task<ActionResult> GetCuentas(int ProgramaID)
            {
                var Cuentas = _context.tblCuenta.Where(c => c.ProgramaID == ProgramaID).ToList();
                return Json(Cuentas);
            }
            [HttpGet]
            public async Task<ActionResult> GetPresupuestos(int CuentaID)
            {
                var Presupuesto = _context.tblPresupuesto.Where(c => c.CuentaID == CuentaID).ToList();
                return Json(Presupuesto);
            }
        [HttpPost]
        public async Task<IActionResult> CrearAsignacion(AsignacionDeCuenta asignacion, int ClientesID, int Grupo, string Tipo)
        {
            Datos();
            ViewBag.ClientesID1 = ClientesID;
            ViewBag.Tipo = Tipo;
            ViewBag.Asignacion = Grupo;
            ViewBag.ClientesID = new SelectList(_context.tblAsignacion.Where(x => x.Clientes.ClientesID.Equals(ClientesID))).FirstOrDefault();

            var Clientes = _context.tblClientes.Select(x => new
            {
                id = x.ClientesID,
                nombre = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno
            }).OrderByDescending(x => x.id);
            ViewBag.ClientesID = new SelectList(Clientes, "id", "nombre");

            var Mensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 100);
            Mensaje.Direction = ParameterDirection.Output;

            await _context.Database
                .ExecuteSqlInterpolatedAsync($@"EXEC DetalleAsignacion
                            @AsignacionID={asignacion.PresupuestoID},
                            @CantidadE={asignacion.PresEntregado},
                            @Mensaje={Mensaje} OUTPUT");

            if (ModelState.IsValid) {
                _context.Add(asignacion);
                _context.SaveChanges();
                return RedirectToAction("CrearInformeSocial", "InformeSocial",
                    new 
                    { 
                        ClientesID = asignacion.ClientesID,
                        Tipo = asignacion.Clientes.TipoDeCliente,
                        Grupo = asignacion.GrupoFamiliar,
                        NumCuenta = asignacion.NumCuenta,
                        NumInforme = asignacion.NumInforme
                    });
            }
            else {
                return View(asignacion);
            }
        }



        public IActionResult CrearAsignacion1(int ClientesID, int NumCuenta, int Grupo, string Tipo, int NumInforme)
        {
            Datos();
            var Cuenta = _context.tblAsignacion.Where(C => C.NumCuenta.Equals(NumCuenta)).FirstOrDefault();
            ViewBag.Tipo = Tipo;
            ViewBag.Grupo = Grupo;
            ViewBag.NumInforme = NumInforme;

            ViewBag.PresEntregado = (from C in _context.tblMedioAyuda where C.ClientesID == ClientesID select C).First().Monto;

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

            return View();
        }
            [HttpGet]
            public async Task<ActionResult> ProgramasLoad1()
            {
                var Programa = _context.tblPrograma.ToList();
                return Json(Programa);
            }
            [HttpGet]
            public async Task<ActionResult> GetCuentas1(int ProgramaID)
            {
                var Cuentas = _context.tblCuenta.Where(c => c.ProgramaID == ProgramaID).ToList();
                return Json(Cuentas);
            }
            [HttpGet]
            public async Task<ActionResult> GetPresupuestos1(int CuentaID)
            {
                var Presupuesto = _context.tblPresupuesto.Where(c => c.CuentaID == CuentaID).ToList();
                return Json(Presupuesto);
            }
        [HttpPost]
        public async Task<IActionResult> CrearAsignacion1(AsignacionDeCuenta asignacion, int ClientesID, int Grupo, int NumCuenta, string Tipo)
        {
            Datos();
            ViewBag.Tipo = Tipo;
            ViewBag.Asignacion = Grupo;
            ViewBag.ClientesID = new SelectList(_context.tblAsignacion.Where(x => x.Clientes.ClientesID.Equals(ClientesID))).FirstOrDefault();

            var Clientes = _context.tblClientes.Select(x => new
            {
                id = x.ClientesID,
                nombre = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno

            }).OrderByDescending(x => x.id);
            ViewBag.ClientesID = new SelectList(Clientes, "id", "nombre");

            var Mensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 100);
            Mensaje.Direction = ParameterDirection.Output;

            await _context.Database
                .ExecuteSqlInterpolatedAsync($@"EXEC DetalleAsignacion
                            @AsignacionID={asignacion.PresupuestoID},
                            @CantidadE={asignacion.PresEntregado},
                            @Mensaje={Mensaje} OUTPUT");

            if (ModelState.IsValid) {
                var Cuenta = _context.tblAsignacion.Where(C => C.NumCuenta.Equals(asignacion.NumCuenta)).FirstOrDefault();
                _context.Add(asignacion);
                _context.SaveChanges();
                return RedirectToAction("CrearInformeSocial", "InformeSocial",
                    new
                    {
                        ClientesID = asignacion.ClientesID,
                        Tipo = asignacion.Clientes.TipoDeCliente,
                        Grupo = asignacion.GrupoFamiliar,
                        NumCuenta = asignacion.NumCuenta,
                        NumInforme = asignacion.NumInforme
                    });
            }
            else {
                return View(asignacion);
            }
        }



        //EDITAR ASIGNACION DE CUENTA
        [HttpGet]
        public IActionResult EditarAsignacion(int AsignacionDeCuentaID, int ClientesID, int NumInforme)
        {
            Datos();
            var asignacion = _context.tblAsignacion
                .Where(C => C.AsignacionDeCuentaID.Equals(AsignacionDeCuentaID))
                .Where(C => C.ClientesID.Equals(ClientesID))
                .Where(C => C.NumInforme.Equals(NumInforme))
                .FirstOrDefault();

            if (asignacion != null) {
                var ultimo = _context.tblClientes.Select(x => new
                {
                    id = x.ClientesID,
                    CodigoYRazon = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno
                }).OrderBy(x => x.id);

                ViewBag.ClientesID = new SelectList(ultimo, "id", "CodigoYRazon");
                return View(asignacion);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarAsignacion(AsignacionDeCuenta asignacion)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblAsignacion
                    .Where(C => C.AsignacionDeCuentaID.Equals(asignacion.AsignacionDeCuentaID))
                    .Where(C => C.ClientesID.Equals(asignacion.ClientesID))
                    .Where(C => C.NumInforme.Equals(asignacion.NumInforme))
                    .FirstOrDefault();

                Editar.AreaGestionID = asignacion.AreaGestionID;
                Editar.ProgramaID = asignacion.ProgramaID;
                Editar.CuentaID = asignacion.CuentaID;
                Editar.PresupuestoID = asignacion.PresupuestoID;
                Editar.PresEntregado = asignacion.PresEntregado;
                Editar.NumCuenta = asignacion.NumCuenta;
                Editar.GrupoFamiliar = asignacion.GrupoFamiliar;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");
            }else {
                return View(asignacion);
            }
        }



        //EDITAR PRESUPUESTO
        [HttpGet]
        public IActionResult EditarPresupuesto(int PresupuestoID)
        {
            Datos();
            var asignacion = _context.tblPresupuesto.Where(C => C.PresupuestoID.Equals(PresupuestoID)).FirstOrDefault();
            if (asignacion != null) {
                return View(asignacion);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult EditarPresupuesto(Presupuesto presupuesto)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblPresupuesto.Where(P => P.PresupuestoID.Equals(presupuesto.PresupuestoID)).FirstOrDefault();

                Editar.PresInicial = presupuesto.PresInicial;
                Editar.PresVigente = presupuesto.PresVigente;

                _context.SaveChanges();
                return RedirectToAction(nameof(IndexPresupuesto));
            }else {
                return View(presupuesto);
            }
        }



        //ELIMINAR ASIGNACION DE CUENTA
        [HttpGet]
        public IActionResult EliminarAsignacion(int ClientesID)
        {
            Datos();
            var asignacion = _context.tblAsignacion.Where(C => C.AsignacionDeCuentaID.Equals(ClientesID)).FirstOrDefault();
            if (asignacion != null) {
                return View(asignacion);
            }else {
                return RedirectToAction("ErrorNotFound", "CrearPDF");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarAsignacion(AsignacionDeCuenta asignacion)
        {
            Datos();
            if (ModelState.IsValid) {
                var asignacioneEliminado = _context.tblAsignacion.Where(C => C.AsignacionDeCuentaID.Equals(asignacion.AsignacionDeCuentaID)).FirstOrDefault();
                _context.Attach(asignacioneEliminado).State = EntityState.Deleted;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexAsignacion));
            }else {
                return View(asignacion);
            }
        }
    }
}
