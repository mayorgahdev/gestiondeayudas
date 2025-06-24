using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    public class GastosFamiliaresController : Controller
    {

        private readonly AppDbContext _context;

        public GastosFamiliaresController(AppDbContext context)
        {
            _context = context;
        }

        private void Datos()
        {
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Rut");
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Pasaporte");
        }


        //[AllowAnonymous]
        public IActionResult IndexGastosFamiliares(int Pagina, string Busqueda)
        {
            Datos();
            var gvm = new GastosFamiliaresView();

            if (Pagina == 0) {
                gvm.Paginas = 1;
            }else {
                gvm.Paginas = Pagina;
            }

            int Muestra = 15;
            int Cantidad = _context.tblGastosFamiliares.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                gvm.CantidadPaginas = Cantidad;
            }else {
                gvm.CantidadPaginas = Cantidad + 1;
            }
            
            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;

            //BUSQUEDA
            var cliente = from c in _context.tblGastosFamiliares select c;
            if (!String.IsNullOrEmpty(Busqueda)) {
                cliente = cliente.Where(c =>
                  c.Clientes.Rut.Contains(Busqueda));
            }

            gvm.listaGastosFamiliares = cliente
                .OrderByDescending(g => g.GastosFamiliaresID)
                .Skip((gvm.Paginas - 1) * Muestra)
                .Take(Muestra)
                .ToList();

            return View(gvm);
        }



        //CREAR GASTOS FAMILIARES
        [HttpGet]
        public IActionResult CrearGastosFamiliares(int Grupo)
        {
            Datos();
            ViewBag.GrupoG = Grupo;
            var ultimo = _context.tblClientes
                .Where(c => c.GrupoFamiliar.Equals(Grupo))
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
        [HttpPost]
        public async Task<IActionResult> CrearGastosFamiliares(GastosFamiliares gastos, int Grupo)
        {
            Datos();
            ViewBag.GrupoG = Grupo;

            var ultimo = _context.tblClientes
                .Where(c => c.GrupoFamiliar.Equals(Grupo))
                .OrderBy(x => x.ClientesID)
                .FirstOrDefault();

            if (ultimo != null) {
                ViewBag.ClientesID = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = ultimo.ClientesID.ToString(),
                        Text = $"{ ultimo.Nombres} {ultimo.ApellidoPaterno} {ultimo.ApellidoMaterno}"
                    }
                }, "Value", "Text");
            }
            else {
                ViewBag.ClientesID = new SelectList(new List<SelectListItem>());
            }

            if (ModelState.IsValid) {
                _context.Add(gastos);
                _context.SaveChanges();
                return RedirectToAction("IndexGrupoFamiliar", "Clientes", 
                    new 
                    {
                        GrupoFamiliar = gastos.GrupoFamiliar
                    });
            }
            else {
                return View(gastos);
            }
        }


        [HttpGet]
        public IActionResult CrearIngresos(int ClientesID, int Grupo, string Tipo)
        {
            Datos();
            ViewBag.Tipo = Tipo;
            ViewBag.Grupoingresos = Grupo;
            ViewBag.ClientesID = new SelectList(_context.tblGastosFamiliares.Where(x => x.Clientes.ClientesID.Equals(ClientesID)));

            var ultimo = _context.tblClientes
                .Where(c => c.GrupoFamiliar.Equals(Grupo))
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

            Ingresos ingresos = new Ingresos();
            string fecha = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            ingresos.FechaElaboracion = DateTime.ParseExact(fecha, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            return View(ingresos);
        }
        [HttpPost]
        public async Task<IActionResult> CrearIngresos(Ingresos ingresos, int Grupo, string Tipo)
        {
            Datos();
            ViewBag.Grupoingresos = Grupo;

            var ultimo = _context.tblClientes
                .Where(c => c.GrupoFamiliar.Equals(Grupo))
                .OrderBy(x => x.ClientesID)
                .FirstOrDefault();

            if (ultimo != null) {
                ViewBag.ClientesID = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = ultimo.ClientesID.ToString(),
                        Text = $"{ ultimo.Nombres} {ultimo.ApellidoPaterno} {ultimo.ApellidoMaterno}"
                    }
                }, "Value", "Text");
            }
            else {
                ViewBag.ClientesID = new SelectList(new List<SelectListItem>());
            }

            if (ModelState.IsValid) {
                _context.Add(ingresos);
                _context.SaveChanges();
                return RedirectToAction("CrearGastosFamiliares", "GastosFamiliares",
                   new
                   {
                       Grupo = ingresos.GrupoFamiliar
                   });
            }
            else {
                return View(ingresos);
            }
        }


        //EDITAR Gastos Familiares
        [HttpGet]
        public IActionResult EditarGastosFamiliares(int Grupo)
        {
            Datos();
            var gastos = _context.tblGastosFamiliares.Where(S => S.GrupoFamiliar.Equals(Grupo)).FirstOrDefault();
            if (gastos != null) {
                var ultimo = _context.tblClientes.Select(x => new
                {
                    id = x.ClientesID,
                    CodigoYRazon = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno
                }).OrderBy(x => x.id);

                ViewBag.ClientesID = new SelectList(ultimo, "id", "CodigoYRazon");
                return View(gastos);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarGastosFamiliares(GastosFamiliares gastosFamiliares)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblGastosFamiliares.Where(S => S.GrupoFamiliar.Equals(gastosFamiliares.GrupoFamiliar)).FirstOrDefault();

                Editar.ClientesID = gastosFamiliares.ClientesID;
                Editar.Alimentacion = gastosFamiliares.Alimentacion;
                Editar.Aseo = gastosFamiliares.Aseo;
                Editar.Arriendo = gastosFamiliares.Arriendo;
                Editar.Dividendo = gastosFamiliares.Dividendo;
                Editar.Servicios = gastosFamiliares.Servicios;
                Editar.Combustible = gastosFamiliares.Combustible;
                Editar.GastosTelefono = gastosFamiliares.GastosTelefono;
                Editar.Movilizacion = gastosFamiliares.Movilizacion;
                Editar.Educacion = gastosFamiliares.Educacion;
                Editar.Creditos = gastosFamiliares.Creditos;
                Editar.Salud = gastosFamiliares.Salud;
                Editar.Varios = gastosFamiliares.Varios;
                Editar.Observaciones = gastosFamiliares.Observaciones;
                Editar.TotalGastosMensuales = gastosFamiliares.TotalGastosMensuales;
                Editar.GrupoFamiliar = gastosFamiliares.GrupoFamiliar;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");
            }else {
                return View();
            }
        }

        [HttpGet]
        public IActionResult EditarIngresos(int Grupo)
        {
            Datos();
            var ingresos = _context.tblIngresos.Where(S => S.GrupoFamiliar.Equals(Grupo)).FirstOrDefault();
            if (ingresos != null) {
                var ultimo = _context.tblClientes.Select(x => new
                {
                    id = x.ClientesID,
                    CodigoYRazon = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno
                }).OrderBy(x => x.id);

                ViewBag.ClientesID = new SelectList(ultimo, "id", "CodigoYRazon");
                return View(ingresos);
            }
            else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarIngresos(Ingresos ingresos)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblIngresos.Where(S => S.GrupoFamiliar.Equals(ingresos.GrupoFamiliar)).FirstOrDefault();

                Editar.IngresosMensuales = ingresos.IngresosMensuales;
                Editar.Descripcion = ingresos.Descripcion;
                Editar.FechaElaboracion = ingresos.FechaElaboracion;
                Editar.GrupoFamiliar = ingresos.GrupoFamiliar;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");
            }
            else {
                return View();
            }
        }



        //ELIMINAR DATOS GASTOS FAMILIARES
        [HttpGet]
        public IActionResult EliminarGastosFamiliares(int ClientesID)
        {
            Datos();
            var informeSocial = _context.tblGastosFamiliares.Where(S => S.ClientesID.Equals(ClientesID)).FirstOrDefault();
            if (informeSocial != null) {
                return View(informeSocial);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarGastosFamiliares(GastosFamiliares gastosFamiliares)
        {
            Datos();
            if (ModelState.IsValid) {
                var EliminarGastosFamiliares = _context.tblGastosFamiliares.Where(S => S.ClientesID.Equals(gastosFamiliares.ClientesID)).FirstOrDefault();
                _context.Attach(EliminarGastosFamiliares).State = EntityState.Deleted;
                _context.SaveChanges();

                return RedirectToAction(nameof(IndexGastosFamiliares));
            }else {
                return View(gastosFamiliares);
            }
        }
    }
}
