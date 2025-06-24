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
    public class SaludController : Controller
    {

        private readonly AppDbContext _context;

        public SaludController(AppDbContext context)
        {
            _context = context;
        }

        private void Datos()
        {
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Rut");
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Pasaporte");
            ViewData["ProcedenciaID"] = new SelectList(_context.tblProcedencia.ToList(), "ProcedenciaID", "Nombre");
            ViewData["DiscapacidadID"] = new SelectList(_context.tblDiscapacidad.ToList(), "DiscapacidadID", "Nombre");
            ViewData["AcreditacionID"] = new SelectList(_context.tblAcreditacion.ToList(), "AcreditacionID", "Nombre");
        }


        public IActionResult IndexSalud(int Pagina, string Busqueda)
        {
            Datos();
            var svm = new SaludView();
            //PAGINADO
            if (Pagina == 0) {
                svm.Paginas = 1;
            } else {
                svm.Paginas = Pagina;
            }
            int Muestra = 15;
            int Cantidad = _context.tblSalud.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                svm.CantidadPaginas = Cantidad;
            } else {
                svm.CantidadPaginas = Cantidad + 1;
            }

            //BUSQUEDA
            var cliente = from c in _context.tblSalud select c;
            if (!String.IsNullOrEmpty(Busqueda)) {
                cliente = cliente.Where(c =>
                  c.Clientes.Rut.Contains(Busqueda));
            }

            svm.listaSalud = cliente
                .OrderByDescending(s => s.SaludID)
                .Skip((svm.Paginas - 1) * Muestra)
                .Take(Muestra)
                .ToList();

            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;

            return View(svm);
        }



        //CREAR SALUD
        [HttpGet]
        public IActionResult CrearSalud(int ClientesID, int Grupo, string Tipo)
        {
            Datos();
            ViewBag.ClientesID = new SelectList(_context.tblSalud.Where(x => x.Clientes.ClientesID.Equals(ClientesID))).FirstOrDefault();
            ViewBag.GrupoSalud = Grupo;
            ViewBag.Tipo = Tipo;

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
            public async Task<ActionResult> DiscapacidadLoad()
            {
                var discapacidad = _context.tblDiscapacidad.ToList();
                return Json(discapacidad);
            }
            [HttpGet]
            public async Task<ActionResult> GetAcreditacion(int DiscapacidadID)
            {
                var acreditacion = _context.tblAcreditacion.Where(c => c.DiscapacidadID == DiscapacidadID).ToList();
                return Json(acreditacion);
            }
        [HttpPost]
        public async Task<IActionResult> CrearSalud(Salud salud, int ClientesID, int Grupo, string Tipo)
        {
            Datos();
            ViewBag.ClientesID = new SelectList(_context.tblSalud.Where(x => x.Clientes.ClientesID.Equals(ClientesID))).FirstOrDefault();
            ViewBag.GrupoSalud = Grupo;
            ViewBag.Tipo = Tipo;

            var Clientes = _context.tblClientes.Select(x => new
            {
                id = x.ClientesID,
                nombre = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno

            }).OrderByDescending(x => x.id);
            ViewBag.ClientesID = new SelectList(Clientes, "id", "nombre");


            if (ModelState.IsValid) {
                _context.Add(salud);
                _context.SaveChanges();
                return RedirectToAction("IndexGrupoFamiliar", "Clientes",
                    new { GrupoFamiliar = salud.GrupoFamiliar });
            } else {
                return View(salud);
            }
        }



        //EDITAR SALUD
        [HttpGet]
        public IActionResult EditarSalud(int ClientesID)
        {
            Datos();
            var Salud = _context.tblSalud.Where(S => S.ClientesID.Equals(ClientesID)).FirstOrDefault();
            if (Salud != null) {
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

                return View(Salud);
            } else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarSalud(Salud salud)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblSalud.Where(C => C.ClientesID.Equals(salud.ClientesID)).FirstOrDefault();

                Editar.ClientesID = salud.ClientesID;
                Editar.ProcedenciaID = salud.ProcedenciaID;
                Editar.DiscapacidadID = salud.DiscapacidadID;
                Editar.AcreditacionID = salud.AcreditacionID;
                Editar.Diagnostico = salud.Diagnostico;
                Editar.Observacion = salud.Observacion;
                Editar.GrupoFamiliar = salud.GrupoFamiliar;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");
            }else {
                return View();
            }
        }



        //ELIMINAR DATOS SALUD
        [HttpGet]
        public IActionResult EliminarSalud(int ClientesID)
        {
            Datos();
            var Salud = _context.tblSalud.Where(S => S.ClientesID.Equals(ClientesID)).FirstOrDefault();
            if (Salud != null)
            {
                return View(Salud);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarSalud(Salud salud)
        {
            Datos();
            if (ModelState.IsValid)
            {
                var SaludeEliminada = _context.tblSalud.Where(C => C.ClientesID.Equals(salud.ClientesID)).FirstOrDefault();
                _context.Attach(SaludeEliminada).State = EntityState.Deleted;
                _context.SaveChanges();

                return RedirectToAction(nameof(IndexSalud));

            }
            else
            {
                return View(salud);
            }
        }
    }
}