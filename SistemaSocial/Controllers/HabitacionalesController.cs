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
    public class HabitacionalesController : Controller
    {

        private readonly AppDbContext _context;

        public HabitacionalesController(AppDbContext context)
        {
            _context = context;
        }

        private void Datos()
        {
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Rut");
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Pasaporte");
            ViewData["SituacionCasaID"] = new SelectList(_context.tblSituacionCasa.ToList(), "SituacionCasaID", "Nombre");
            ViewData["TipoViviendaID"] = new SelectList(_context.tblTipoVivienda.ToList(), "TipoViviendaID", "Nombre");
            ViewData["MaterialViviendaID"] = new SelectList(_context.tblMaterialVivienda.ToList(), "MaterialViviendaID", "Nombre");
            ViewData["TenenciaTerrenoID"] = new SelectList(_context.tblTenenciaTerreno.ToList(), "TenenciaTerrenoID", "Nombre");
        }


        //[AllowAnonymous]
        public IActionResult IndexHabitacionales(int Pagina, string Busqueda)
        {
            Datos();
            var hvm = new HabitacionalesView();
            if (Pagina == 0) {
                hvm.Paginas = 1;
            }else {
                hvm.Paginas = Pagina;
            }

            int Muestra = 15;
            int Cantidad = _context.tblHabitacionales.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                hvm.CantidadPaginas = Cantidad;
            } else {
                hvm.CantidadPaginas = Cantidad + 1;
            }

            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;

            //BUSQUEDA
            var cliente = from c in _context.tblHabitacionales select c;
            if (!String.IsNullOrEmpty(Busqueda)) {
                cliente = cliente.Where(c =>
                  c.Clientes.Rut.Contains(Busqueda));
            }

            hvm.listaHabitacionales = cliente
                .OrderByDescending(h => h.HabitacionalesID)
                .Skip((hvm.Paginas - 1) * Muestra)
                .Take(Muestra)
                .ToList();

            return View(hvm);
        }



        //CREAR HABITACIONALES
        [HttpGet]
        public IActionResult CrearHabitacionales(int Grupo)
        {
            Datos();
            ViewBag.GrupoH = Grupo;
            var ultimo = _context.tblClientes
                .Where(c => c.GrupoFamiliar.Equals(Grupo))
                .OrderBy(x => x.ClientesID)
                .FirstOrDefault();

            ViewBag.ClientesID = new SelectList( new List<SelectListItem>
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
        public async Task<IActionResult> CrearHabitacionales(Habitacionales habitacionales, int Grupo)
        {
            Datos();
            ViewBag.GrupoH = Grupo;
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
                _context.Add(habitacionales);
                _context.SaveChanges();
                return RedirectToAction("CrearGastosFamiliares", "GastosFamiliares",
                    new 
                    { 
                        Grupo = habitacionales.GrupoFamiliar
                    });
            }
            else {
                return View(habitacionales);
            }
        }


        //CREAR SITUACION ACTUAL
        [HttpGet]
        public IActionResult CrearSituacionActual(int Grupo)
        {
            Datos();
            ViewBag.GrupoS = Grupo;
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

            ViewBag.GrupoH = Grupo;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearSituacionActual(Situacion situacion, int Grupo)
        {
            Datos();
            ViewBag.GrupoS = Grupo;
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
                _context.Add(situacion);
                _context.SaveChanges();
                return RedirectToAction("CrearHabitacionales", "Habitacionales", 
                new
                {
                    Grupo = situacion.GrupoFamiliar
                });
            }
            else{
                return View(situacion);
            }
        }



        //EDITAR HABITACIONALES
        [HttpGet]
        public IActionResult EditarHabitacionales(int Grupo)
        {
            Datos();
            var Habitacionales = _context.tblHabitacionales.Where(S => S.GrupoFamiliar.Equals(Grupo)).FirstOrDefault();
            if (Habitacionales != null) {
                var ultimo = _context.tblClientes.Select(x => new
                {
                    id = x.ClientesID,
                    CodigoYRazon = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno
                }).OrderBy(x => x.id);

                ViewBag.ClientesID = new SelectList(ultimo, "id", "CodigoYRazon");
                return View(Habitacionales);
            }
            else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarHabitacionales(Habitacionales habitacionales)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblHabitacionales.Where(S => S.GrupoFamiliar.Equals(habitacionales.GrupoFamiliar)).FirstOrDefault();

                Editar.ClientesID = habitacionales.ClientesID;
                Editar.SituacionCasaID = habitacionales.SituacionCasaID;

                Editar.Luzirregular = habitacionales.Luzirregular;
                Editar.RedPublica = habitacionales.RedPublica;
                Editar.SinLuz = habitacionales.SinLuz;

                Editar.Agua = habitacionales.Agua;
                Editar.AguaCamion = habitacionales.AguaCamion;
                Editar.AguaNoria = habitacionales.AguaNoria;
                Editar.AguaVertiente = habitacionales.AguaVertiente;

                Editar.Alcantarillado = habitacionales.Alcantarillado;
                Editar.Fosa = habitacionales.Fosa;
                Editar.Pozo = habitacionales.Pozo;
                Editar.Otro = habitacionales.Otro;

                Editar.TipoViviendaID = habitacionales.TipoViviendaID;
                Editar.MaterialViviendaID = habitacionales.MaterialViviendaID;
                Editar.Definir = habitacionales.Definir;
                Editar.NumDormitorios = habitacionales.NumDormitorios;
                Editar.TotalDependecias = habitacionales.TotalDependecias;
                Editar.NivelAsinamiento = habitacionales.NivelAsinamiento;
                Editar.TenenciaTerrenoID = habitacionales.TipoViviendaID;
                Editar.Observaciones = habitacionales.Observaciones;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");
            }
            else {
                return View();
            }
        }



        //EDITAR SITUACION ACTUAL
        [HttpGet]
        public IActionResult EditarSituacion(int Grupo)
        {
            Datos();
            var situacion = _context.tblSituacionActual.Where(S => S.GrupoFamiliar.Equals(Grupo)).FirstOrDefault();
            if (situacion != null) {
                var ultimo = _context.tblClientes.Select(x => new
                {
                    id = x.ClientesID,
                    CodigoYRazon = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno
                }).OrderBy(x => x.id);

                ViewBag.ClientesID = new SelectList(ultimo, "id", "CodigoYRazon");
                return View(situacion);
            }
            else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarSituacion(Situacion situacion)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblSituacionActual.Where(S => S.GrupoFamiliar.Equals(situacion.GrupoFamiliar)).FirstOrDefault();

                Editar.ClientesID = situacion.ClientesID;
                Editar.SituacionActual = situacion.SituacionActual;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");
            }
            else {
                return View();
            }
        }



        //ELIMINAR DATOS HABBITACIONALES
        [HttpGet]
        public IActionResult EliminarHabitacionales(int ClientesID)
        {
            Datos();
            var Habitacionales = _context.tblHabitacionales.Where(S => S.ClientesID.Equals(ClientesID)).FirstOrDefault();
            if (Habitacionales != null) {
                return View(Habitacionales);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarHabitacionales(Habitacionales habitacionales)
        {
            Datos();
            if (ModelState.IsValid) {
                var HabitacionalEliminado = _context.tblHabitacionales.Where(C => C.ClientesID.Equals(habitacionales.ClientesID)).FirstOrDefault();
                _context.Attach(HabitacionalEliminado).State = EntityState.Deleted;
                _context.SaveChanges();

                return RedirectToAction(nameof(IndexHabitacionales));

            }else {
                return View(habitacionales);
            }
        }
    }
}
