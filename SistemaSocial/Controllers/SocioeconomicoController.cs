using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSocial.Models;
using SistemaSocial.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Controllers
{
    [Authorize(Policy = "PolicyAdmin")]
    public class SocioeconomicoController : Controller
    {

        private readonly AppDbContext _context;

        public SocioeconomicoController(AppDbContext context)
        {
            _context = context;
        }


        private void Datos()
        {
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Rut");
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Pasaporte");
            ViewData["TipoPensionID"] = new SelectList(_context.tblTipoPension
                                                 .OrderByDescending(x => x.Nombre != "Otra.")
                                                 .ThenByDescending(x => x.Nombre != "Sin Pensión.")
                                                 .ThenBy(x => x.Nombre)
                                                 .ToList(), "TipoPensionID", "Nombre");

            ViewData["TipoSubsidioID"] = new SelectList(_context.tblTipoSubsidio
                                                   .OrderByDescending(x => x.Nombre != "Otra.")
                                                   .ThenByDescending(x => x.Nombre != "Sin Subsidio.")
                                                   .ThenBy(x => x.Nombre)
                                                   .ToList(), "TipoSubsidioID", "Nombre");
        }


        //[AllowAnonymous]
        public IActionResult IndexSocioeconomico(int Pagina, string Busqueda)
        {
            Datos();
            var evm = new SocioeconomicoView();
            if (Pagina == 0) {
                evm.Paginas = 1;
            }else {
                evm.Paginas = Pagina;
            }

            int Muestra = 15;
            int Cantidad = _context.tblSocioeconomico.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                evm.CantidadPaginas = Cantidad;
            }else {
                evm.CantidadPaginas = Cantidad + 1;
            }

            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;

            //BUSQUEDA
            var cliente = from c in _context.tblSocioeconomico select c;
            if (!String.IsNullOrEmpty(Busqueda)) {
                cliente = cliente.Where(c =>
                c.Clientes.Rut.Contains(Busqueda));
            }



            evm.listaSocioeconomico = cliente
                .OrderByDescending(e => e.SocioeconomicoID)
                .Skip((evm.Paginas - 1) * Muestra)
                .Take(Muestra)              
                .ToList();

            return View(evm);
        }



        //CREAR ECONOMICO SOCIAL
        [HttpGet]
        public IActionResult CrearSocioeconomico(int ClientesID, int Grupo, string Tipo)
        {
            Datos();
            ViewBag.ClientesID = new SelectList(_context.tblSocioeconomico.Where(x => x.Clientes.ClientesID.Equals(ClientesID))).FirstOrDefault();
            ViewBag.GrupoSE = Grupo;
            ViewBag.Tipo = Tipo;

            ViewBag.Actividad = (from C in _context.tblClientes where C.ClientesID == ClientesID select C).OrderByDescending(x => x.ClientesID).First().Ocupacion;

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
        [HttpPost]
        public async Task<IActionResult> CrearSocioeconomico(Socioeconomico socioeconomico, int ClientesID, int Grupo, string Tipo)
        {
            Datos();
            ViewBag.ClientesID = new SelectList(_context.tblSocioeconomico.Where(x => x.Clientes.ClientesID.Equals(ClientesID))).FirstOrDefault();
            ViewBag.GrupoSE = Grupo;
            ViewBag.Tipo = Tipo;

            var Clientes = _context.tblClientes.Select(x => new
            {
                id = x.ClientesID,
                nombre = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno

            }).OrderByDescending(x => x.id);
            ViewBag.ClientesID = new SelectList(Clientes, "id", "nombre");

            if (ModelState.IsValid) {
                _context.Add(socioeconomico);
                _context.SaveChanges();
                return RedirectToAction("IndexGrupoFamiliar", "Clientes",
                    new { GrupoFamiliar = socioeconomico.GrupoFamiliar });
            }
            else {
                return View(socioeconomico);
            }
        }



        //EDITAR ECONOMICO SOCIAL
        [HttpGet]
        public IActionResult EditarSocioeconomico(int ClientesID)
        {
            Datos();
            var Socioeconomico = _context.tblSocioeconomico.Where(E => E.ClientesID.Equals(ClientesID)).FirstOrDefault();
            if (Socioeconomico != null) {
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

                return View(Socioeconomico);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarSocioeconomico(Socioeconomico socioeconomico)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblSocioeconomico.Where(E => E.ClientesID.Equals(socioeconomico.ClientesID)).FirstOrDefault();

                Editar.ClientesID = socioeconomico.ClientesID;

                Editar.Actividad = socioeconomico.Actividad;
                Editar.IngresoActividad = socioeconomico.IngresoActividad;

                Editar.TipoPensionID = socioeconomico.TipoPensionID;
                Editar.OtrosIngresos1 = socioeconomico.OtrosIngresos1;

                Editar.TipoSubsidioID = socioeconomico.TipoSubsidioID;
                Editar.OtrosIngresos2 = socioeconomico.OtrosIngresos2;

                Editar.TipoIngreso1 = socioeconomico.TipoIngreso1;
                Editar.OtrosIngresos3 = socioeconomico.OtrosIngresos3;

                Editar.TipoIngreso2 = socioeconomico.TipoIngreso2;
                Editar.OtrosIngresos4 = socioeconomico.OtrosIngresos4;

                Editar.TotalIngresos = socioeconomico.TotalIngresos;
                Editar.Observacion = socioeconomico.Observacion;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");
            }else {
                return View();
            }
        }



        //ELIMINAR DATOS ECONOMICOS Y SOCIALES
        [HttpGet]
        public IActionResult EliminarSocioeconomico(int ClientesID)
        {
            Datos();
            var EconomicoSocial = _context.tblSocioeconomico.Where(E => E.ClientesID.Equals(ClientesID)).FirstOrDefault();
            if (EconomicoSocial != null) {
                return View(EconomicoSocial);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarSocioeconomico(Socioeconomico socioeconomico)
        {
            Datos();
            if (ModelState.IsValid) {
                var EconomicoSocialEliminado = _context.tblSocioeconomico.Where(E => E.ClientesID.Equals(socioeconomico.ClientesID)).FirstOrDefault();
                _context.Attach(EconomicoSocialEliminado).State = EntityState.Deleted;
                _context.SaveChanges();

                return RedirectToAction(nameof(IndexSocioeconomico));

            }else {
                return View(socioeconomico);
            }
        }
    }
}
