using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Controllers
{
    [Authorize(Policy = "PolicyAdmin")]
    public class MedioAyudaController : Controller
    {

        private readonly AppDbContext _context;

        public MedioAyudaController(AppDbContext context)
        {
            _context = context;
        }

        private void Datos()
        {
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Rut");
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Pasaporte");
        }


        public IActionResult IndexMedioAyuda()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CrearMedioAyuda(int ClientesID, int Grupo, string Tipo, int NumInforme)
        {
            Datos();
            ViewBag.ClientesID1 = ClientesID;
            ViewBag.Tipo = Tipo;
            ViewBag.GrupoMedio = Grupo;
            ViewBag.NumInforme = NumInforme;

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
        public async Task<IActionResult> CrearMedioAyuda(MedioAyuda MedioAyuda, int ClientesID, int Grupo, string Tipo)
        {
            Datos();
            ViewBag.Tipo = Tipo;
            ViewBag.GrupoMedio = Grupo;
            ViewBag.ClientesID = new SelectList(_context.tblMedioAyuda.Where(x => x.Clientes.ClientesID.Equals(ClientesID))).FirstOrDefault();

            var Clientes = _context.tblClientes.Select(x => new
            {
                id = x.ClientesID,
                nombre = x.Nombres,
                x.ApellidoPaterno,
                x.ApellidoMaterno

            }).OrderByDescending(x => x.id);
            ViewBag.ClientesID = new SelectList(Clientes, "id", "nombre");

            if (ModelState.IsValid) {
                _context.Add(MedioAyuda);
                _context.SaveChanges();
                return RedirectToAction("CrearAsignacion", "AsignacionDeCuentas",
                    new
                    {
                        ClientesID = MedioAyuda.ClientesID,
                        Grupo = MedioAyuda.GrupoFamiliar,
                        NumMedio = MedioAyuda.NumMedio,
                        NumInforme = MedioAyuda.NumInforme
                    });
            }
            else {
                return View(MedioAyuda);
            }
        }


        [HttpGet]
        public IActionResult CrearMedioAyuda1(int ClientesID, string Tipo, int Grupo, int NumMedio, int NumInforme)
        {
            Datos();
            var Cuenta = _context.tblMedioAyuda.Where(C => C.NumMedio.Equals(NumMedio)).FirstOrDefault();
            ViewBag.Tipo = Tipo;
            ViewBag.GrupoMedio = Grupo;
            ViewBag.NumInforme = NumInforme;

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
        public async Task<IActionResult> CrearMedioAyuda1(MedioAyuda MedioAyuda, int ClientesID, string Tipo, int Grupo, int NumMedio)
        {
            Datos();
            ViewBag.ClientesID1 = ClientesID;
            ViewBag.Tipo = Tipo;
            ViewBag.GrupoMedio = Grupo;
            ViewBag.ClientesID = new SelectList(_context.tblMedioAyuda.Where(x => x.Clientes.ClientesID.Equals(ClientesID))).FirstOrDefault();

            var Clientes = _context.tblClientes.Select(x => new
            {
                id = x.ClientesID,
                nombre = x.Nombres,
                x.ApellidoPaterno,
                x.ApellidoMaterno

            }).OrderByDescending(x => x.id);
            ViewBag.ClientesID = new SelectList(Clientes, "id", "nombre");

            if (ModelState.IsValid) {
                var Cuenta = _context.tblMedioAyuda.Where(C => C.NumMedio.Equals(MedioAyuda.NumMedio)).FirstOrDefault();
                _context.Add(MedioAyuda);
                _context.SaveChanges();
                return RedirectToAction("CrearAsignacion", "AsignacionDeCuentas",
                    new
                    {
                        ClientesID = MedioAyuda.ClientesID,
                        Grupo = MedioAyuda.GrupoFamiliar,
                        NumMedio = MedioAyuda.NumMedio,
                        NumInforme = MedioAyuda.NumInforme
                    });
            }
            else {
                return View(MedioAyuda);
            }
        }



        //EDITAR INFORME SOCIAL
        [HttpGet]
        public IActionResult EditarMedioAyuda(int MedioID, int ClientesID, int NumInforme)
        {
            Datos();
            var MedioAyuda = _context.tblMedioAyuda
                .Where(S => S.MedioID.Equals(MedioID))
                .Where(S => S.ClientesID.Equals(ClientesID))
                .Where(S => S.NumInforme.Equals(NumInforme))
                .FirstOrDefault();

            if (MedioAyuda != null) {
                var ultimo = _context.tblClientes.Select(x => new
                {
                    id = x.ClientesID,
                    CodigoYRazon = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno
                }).OrderBy(x => x.id);

                ViewBag.ClientesID = new SelectList(ultimo, "id", "CodigoYRazon");
                return View(MedioAyuda);
            }
            else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarMedioAyuda(MedioAyuda medioAyuda)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblMedioAyuda
                    .Where(S => S.MedioID.Equals(medioAyuda.MedioID))
                    .Where(S => S.ClientesID.Equals(medioAyuda.ClientesID))
                    .Where(S => S.NumInforme.Equals(medioAyuda.NumInforme))
                    .FirstOrDefault();

                Editar.ClientesID = medioAyuda.ClientesID;
                Editar.Monto = medioAyuda.Monto;
                Editar.Orden = medioAyuda.Orden;
                Editar.Descripcion = medioAyuda.Descripcion;
                Editar.NumInforme = medioAyuda.NumInforme;
                Editar.GrupoFamiliar = medioAyuda.GrupoFamiliar;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");

            }else {
                return View();
            }
        }
    }
}
