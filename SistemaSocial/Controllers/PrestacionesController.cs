using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaSocial.Models;
using SistemaSocial.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaWebSocial.Controllers
{
    [Authorize(Policy = "PolicyAdmin")]
    public class PrestacionesController : Controller
    {
        private readonly AppDbContext _context;

        public PrestacionesController(AppDbContext context)
        {
            _context = context;
        }

        //CARGAR PRESTACIONES
        public IActionResult IndexPrestaciones(int Pagina, string Busqueda)
        {
            var pvm = new PrestacionesView();

            if (Pagina == 0) {
                pvm.Paginas = 1;
            }else {
                pvm.Paginas = Pagina;
            }
            int Muestra = 17;
            int Cantidad = _context.tblPrestaciones.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                pvm.CantidadPaginas = Cantidad;
            }else {
                pvm.CantidadPaginas = Cantidad + 1;
            }

            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;

            //BUSQUEDA
            var cliente = from c in _context.tblPrestaciones select c;
            if (!String.IsNullOrEmpty(Busqueda)) {
                cliente = cliente.Where(c =>
                  c.Nombre.Contains(Busqueda));
            }

            pvm.listaPrestaciones = cliente
                .Skip((pvm.Paginas - 1) * Muestra)
                .Take(Muestra)
                .ToList();

            return View(pvm);
        }



        //CREAR PRESTACIONES
        [HttpGet]
        public IActionResult CrearPrestaciones()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearPrestaciones(Prestaciones prestaciones)
        {
            if (ModelState.IsValid) {
                _context.Add(prestaciones);
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexPrestaciones));
            }else {
                return View(prestaciones);
            }
        }



        //EDITAR PRESTACIONES
        [HttpGet]
        public IActionResult EditarPrestaciones(int PrestacionesID)
        {
            var Prestaciones = _context.tblPrestaciones.Where(P => P.PrestacionesID.Equals(PrestacionesID)).FirstOrDefault();
            if (Prestaciones != null) {
                return View(Prestaciones);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarPrestaciones(Prestaciones prestaciones)
        {
            if (ModelState.IsValid) {
                var Editar = _context.tblPrestaciones.Where(P => P.PrestacionesID.Equals(prestaciones.PrestacionesID)).FirstOrDefault();

                Editar.Nombre = prestaciones.Nombre;
                Editar.Cantidad = prestaciones.Cantidad;

                _context.SaveChanges();
                return RedirectToAction(nameof(IndexPrestaciones));

            }else {
                return View(prestaciones);
            }

        }



        //ELIMINAR PRESTACIONES
        [HttpGet]
        public IActionResult EliminarPrestaciones(int PrestacionesID)
        {
            var Prestaciones = _context.tblPrestaciones.Where(P => P.PrestacionesID.Equals(PrestacionesID)).FirstOrDefault();
            if (Prestaciones != null) {
                return View(Prestaciones);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPrestaciones(Prestaciones prestaciones)
        {
            if (ModelState.IsValid) {
                var Eliminar = _context.tblPrestaciones.Where(P => P.PrestacionesID.Equals(prestaciones.PrestacionesID)).FirstOrDefault();
                _context.Attach(Eliminar).State = EntityState.Deleted;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexPrestaciones));
            }else {
                return View(prestaciones);
            }
        }








        //PENSIONES Y SUBSIDIOS
        public IActionResult IndexPension_Y_Subsidio()
        {
            var pension = _context.tblTipoPension.OrderByDescending(x => x.Nombre != "Otra.")
                                                 .ThenByDescending(x => x.Nombre != "Sin Pensión.")
                                                 .ThenBy(x => x.Nombre)
                                                 .ToList();

            var subsidio = _context.tblTipoSubsidio.OrderByDescending(x => x.Nombre != "Otra.")
                                                   .ThenByDescending(x => x.Nombre != "Sin Subsidio.")
                                                   .ThenBy(x => x.Nombre)
                                                   .ToList();

            var PYS = new Pension_Y_Subsidio
            {
                ListPension = pension,
                ListSubsidio = subsidio
            };

            return View(PYS);
        }

        //PENSION
        [HttpGet]
        public IActionResult CrearPension()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearPension(TipoPension pension)
        {
            if (ModelState.IsValid) {
                _context.Add(pension);
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexPension_Y_Subsidio));
            }
            else {
                return View(pension);
            }
        }


        [HttpGet]
        public IActionResult EditarPension(int TipoPensionID)
        {
            var pension = _context.tblTipoPension.Where(P => P.TipoPensionID.Equals(TipoPensionID)).FirstOrDefault();
            if (pension != null) {
                return View(pension);
            }
            else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarPension(TipoPension pension)
        {
            if (ModelState.IsValid) {
                var Editar = _context.tblTipoPension.Where(P => P.TipoPensionID.Equals(pension.TipoPensionID)).FirstOrDefault();

                Editar.Nombre = pension.Nombre;

                _context.SaveChanges();
                return RedirectToAction(nameof(IndexPension_Y_Subsidio));

            }
            else {
                return View(pension);
            }

        }


        [HttpGet]
        public IActionResult EliminarPension(int TipoPensionID)
        {
            var pension = _context.tblTipoPension.Where(P => P.TipoPensionID.Equals(TipoPensionID)).FirstOrDefault();
            if (pension != null) {
                return View(pension);
            }
            else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPension(TipoPension pension)
        {
            if (ModelState.IsValid) {
                var Eliminar = _context.tblTipoPension.Where(P => P.TipoPensionID.Equals(pension.TipoPensionID)).FirstOrDefault();
                _context.Attach(Eliminar).State = EntityState.Deleted;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexPension_Y_Subsidio));
            }
            else {
                return View(pension);
            }
        }




        //SUBSIDIOS
        [HttpGet]
        public IActionResult CrearSubsidio()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearSubsidio(TipoSubsidio subsidio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subsidio);
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexPension_Y_Subsidio));
            }
            else
            {
                return View(subsidio);
            }
        }


        [HttpGet]
        public IActionResult EditarSubsidio(int TipoSubsidioID)
        {
            var subsidio = _context.tblTipoSubsidio.Where(P => P.TipoSubsidioID.Equals(TipoSubsidioID)).FirstOrDefault();
            if (subsidio != null) {
                return View(subsidio);
            }
            else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarSubsidio(TipoSubsidio subsidio)
        {
            if (ModelState.IsValid) {
                var Editar = _context.tblTipoSubsidio.Where(P => P.TipoSubsidioID.Equals(subsidio.TipoSubsidioID)).FirstOrDefault();

                Editar.Nombre = subsidio.Nombre;

                _context.SaveChanges();
                return RedirectToAction(nameof(IndexPension_Y_Subsidio));

            }
            else {
                return View(subsidio);
            }

        }


        [HttpGet]
        public IActionResult EliminarSubsidio(int TipoSubsidioID)
        {
            var subsidio = _context.tblTipoSubsidio.Where(P => P.TipoSubsidioID.Equals(TipoSubsidioID)).FirstOrDefault();
            if (subsidio != null)
            {
                return View(subsidio);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarSubsidio(TipoSubsidio subsidio)
        {
            if (ModelState.IsValid) {
                var Eliminar = _context.tblTipoSubsidio.Where(P => P.TipoSubsidioID.Equals(subsidio.TipoSubsidioID)).FirstOrDefault();
                _context.Attach(Eliminar).State = EntityState.Deleted;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexPension_Y_Subsidio));
            }
            else {
                return View(subsidio);
            }
        }
    }
}
