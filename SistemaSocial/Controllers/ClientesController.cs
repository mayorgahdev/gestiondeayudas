using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        private void Datos()
        {
            ViewData["NacionalidadID"] = new SelectList(_context.tblNacionalidad.ToList(), "NacionalidadID", "Nombre");
            ViewData["PrevisionSocialID"] = new SelectList(_context.tblPrevisionSocial.ToList(), "PrevisionSocialID", "Nombre");
            ViewData["PrevisionSaludID"] = new SelectList(_context.tblPrevisionSalud.ToList(), "PrevisionSaludID", "Nombre");
            ViewData["IndiceEscolaridadID"] = new SelectList(_context.tblIndiceEscolaridad.ToList(), "IndiceEscolaridadID", "Nombre");
            ViewData["ParentescoID"] = new SelectList(_context.tblParentesco.ToList(), "ParentescoID", "Nombre");
            ViewData["EstadoCivilID"] = new SelectList(_context.tblEstadoCivil.ToList(), "EstadoCivilID", "Nombre");

            ViewData["RSHID"] = new SelectList(_context.tblRSH.ToList(), "RSHID", "Nombre");
            ViewData["TramoID"] = new SelectList(_context.tblTramo.ToList(), "TramoID", "Nombre");
        }
        private void DatosValidar(Clientes clientes)
        {
            var validarRut = _context.tblClientes.FirstOrDefault(c => c.Rut == clientes.Rut);
            //var validarGrupo = _context.tblClientes.FirstOrDefault(c => c.GrupoFamiliar == clientes.GrupoFamiliar);
            if (validarRut != null) {
                ModelState.AddModelError("Rut", "El Run Ingresado ya se encuentra registrado en el Sistema");
            }

            //if (validarGrupo != null) {
            //    ModelState.AddModelError("GrupoFamiliar", "El N° Asignado ya se encuentra registrado en el Sistema");
            //}
        }


        [HttpGet]
        public IActionResult ListadoUsuarios(int Pagina, string Busqueda)
        {
            Datos();
            var cvm = new ClientesView();

            //PAGINADO
            if (Pagina == 0) {
                cvm.Paginas = 1;
            }
            else {
                cvm.Paginas = Pagina;
            }
            int Muestra = 8;
            int Cantidad = _context.tblClientes.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                cvm.CantidadPaginas = Cantidad;
            }
            else {
                cvm.CantidadPaginas = Cantidad + 1;
            }
            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;


            //BUSQUEDA
            var cliente = from c in _context.tblClientes select c;
            if (!String.IsNullOrEmpty(Busqueda)) {
                cliente = cliente.Where(c =>
                  c.Rut.Contains(Busqueda)
               || c.Nombres.Contains(Busqueda)
               || c.ApellidoPaterno.Contains(Busqueda));
            }

            cvm.listaClientes = cliente
                .OrderByDescending(c => c.ClientesID)
                .Skip((cvm.Paginas - 1) * Muestra)
                .Take(Muestra)
                .ToList();

            return View(cvm);
        }
        [HttpGet]
        public IActionResult IndexClientes(int Pagina, string Busqueda)
        {
            Datos();
            var cvm = new ClientesView();

            //PAGINADO
            if (Pagina == 0) {
                cvm.Paginas = 1;
            }
            else {
                cvm.Paginas = Pagina;
            }
            int Muestra = 8;
            int Cantidad = _context.tblClientes.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                cvm.CantidadPaginas = Cantidad;
            }
            else {
                cvm.CantidadPaginas = Cantidad + 1;
            }
            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;


            //BUSQUEDA
            var cliente = from c in _context.tblClientes select c;
            if (!String.IsNullOrEmpty(Busqueda)) {
                cliente = cliente.Where(c =>
                  c.Rut.Contains(Busqueda)
               || c.Nombres.Contains(Busqueda)
               || c.ApellidoPaterno.Contains(Busqueda));
            }

            var clientes = cliente
                .Where(c => c.TipoDeCliente.Equals("Requirente") || c.TipoDeCliente.Equals("Ambos"))
                .OrderBy(c => c.ClientesID)
                .ToList();

            var GrupoFamiliar = cliente
                .Where(c => c.TipoDeCliente.Equals("Requirente") || c.TipoDeCliente.Equals("Ambos"))
                .Select(c => c.GrupoFamiliar)
                .Distinct()
                .ToList();

            cvm.listaClientes = GrupoFamiliar
                .Select(grupo => clientes.FirstOrDefault(c => c.GrupoFamiliar == grupo))
                .OrderByDescending(c => c.ClientesID)
                .Skip((cvm.Paginas - 1) * Muestra)
                .Take(Muestra)
                .ToList();

            return View(cvm);
        }
        [HttpGet]
        public IActionResult IndexGrupoFamiliar(int GrupoFamiliar, string Busqueda)
        {
            if (ModelState.IsValid) {
                Datos();
                var cvm = new ClientesView();

                //BUSQUEDA
                var cliente = from c in _context.tblClientes select c;

                if (!String.IsNullOrEmpty(Busqueda)) {
                    cliente = cliente.Where(c =>
                      c.Rut.Contains(Busqueda)
                   || c.Nombres.Contains(Busqueda)
                   || c.ApellidoPaterno.Contains(Busqueda));
                }
                cvm.listaClientes = cliente
                    .Where(C => C.GrupoFamiliar.Equals(GrupoFamiliar))
                    .ToList();
                return View(cvm);
            }
            else {
                return NotFound();
            }
        }



        //CREAR UN NUEVO CLIENTE
        [HttpGet]
        public IActionResult CrearClientes(int ClientesID)
        {
            Datos();
            ViewBag.ClientesID1 = ClientesID;
            ViewBag.ParentescoID = new SelectList(_context.tblParentesco, "ParentescoID", "Nombre", 13);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearClientes(Clientes clientes)
        {
            Datos();
            DatosValidar(clientes);
            if (ModelState.IsValid) {
                _context.Add(clientes);
                _context.SaveChanges();
                return RedirectToAction("IndexGrupoFamiliar", "Clientes",
                    new { GrupoFamiliar = clientes.GrupoFamiliar });
            }
            else {
                return View(clientes);
            }
        }


        
        //AGREGAR GRUPO FAMILIAR 
        [HttpGet]
        public IActionResult CrearGrupoFamiliar(int GrupoFamiliar)
        {
            Datos();
            var Cliente = _context.tblClientes.Where(C => C.GrupoFamiliar.Equals(GrupoFamiliar)).FirstOrDefault();
            ViewBag.Direccion = (from C in _context.tblClientes where C.GrupoFamiliar == GrupoFamiliar select C).First().Direccion;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearGrupoFamiliar(Clientes clientes)
        {
            Datos();
            if (ModelState.IsValid) {
                var Grupo = _context.tblClientes.Where(C => C.GrupoFamiliar.Equals(clientes.GrupoFamiliar)).FirstOrDefault();
                _context.Add(clientes);
                _context.SaveChanges();
                return RedirectToAction("IndexGrupoFamiliar", "Clientes",
                    new { GrupoFamiliar = clientes.GrupoFamiliar });
            }
            else {
                return View(clientes);
            }
        }


        //AGREGAR ANTECEDENTES CRONICOS
        [HttpGet]
        public IActionResult CrearCronicos(int Grupo)
        {
            Datos();
            ViewBag.GrupoCro = Grupo;
            return View();
        }
            [HttpGet]
            public async Task<ActionResult> RSHLoad()
            {
                var RSH = _context.tblRSH.ToList();
                return Json(RSH);
            }
            [HttpGet]
            public async Task<ActionResult> GetTramos(int RSHID)
            {
                var tramos = _context.tblTramo.Where(c => c.RSHID == RSHID).ToList();
                return Json(tramos);
            }
        [HttpPost]
        public async Task<IActionResult> CrearCronicos(Cronicos cronicos, int Grupo)
        {
            Datos();
            ViewBag.GrupoCro = Grupo;
            if (ModelState.IsValid) {
                _context.Add(cronicos);
                _context.SaveChanges();
                return RedirectToAction("IndexGrupoFamiliar", "Clientes",
                    new { GrupoFamiliar = cronicos.GrupoFamiliar });
            }
            else {
                return View(cronicos);
            }
        }


        //EDITAR CLIENTE
        [HttpGet]
        public IActionResult EditarClientes(int ClientesID)
        {
            Datos();
            var Cliente = _context.tblClientes.Where(C => C.ClientesID.Equals(ClientesID)).FirstOrDefault();
            if (Cliente != null) {
                return View(Cliente);
            } 
            else {
                return RedirectToAction(nameof(IndexClientes));
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarClientes(Clientes clientes)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblClientes.Where(C => C.ClientesID.Equals(clientes.ClientesID)).FirstOrDefault();

                Editar.Rut = clientes.Rut;
                Editar.TipoDeCliente = clientes.TipoDeCliente;
                Editar.Nombres = clientes.Nombres;
                Editar.ApellidoPaterno = clientes.ApellidoPaterno;
                Editar.ApellidoMaterno = clientes.ApellidoMaterno;
                Editar.FechaNacimiento = clientes.FechaNacimiento;
                Editar.Edad = clientes.Edad;
                Editar.NacionalidadID = clientes.NacionalidadID;
                Editar.Direccion = clientes.Direccion;
                Editar.Telefono = clientes.Telefono;
                Editar.Correo = clientes.Correo;
                Editar.EstadoCivilID = clientes.EstadoCivilID;
                Editar.PrevisionSocialID = clientes.PrevisionSocialID;
                Editar.PrevisionSaludID = clientes.PrevisionSaludID;
                Editar.IndiceEscolaridadID = clientes.IndiceEscolaridadID;
                Editar.ParentescoID = clientes.ParentescoID;
                Editar.Ocupacion = clientes.Ocupacion;
                Editar.GrupoFamiliar = clientes.GrupoFamiliar;

                _context.SaveChanges();
                return RedirectToAction("IndexGrupoFamiliar", "Clientes",
                    new { GrupoFamiliar = clientes.GrupoFamiliar });
            }
            else {
                return View();
            }
        }

        //EDITAR CRONICOS
        [HttpGet]
        public IActionResult EditarCronicos(int Grupo)
        {
            Datos();
            var cronicos = _context.tblCronicos.Where(S => S.GrupoFamiliar.Equals(Grupo)).FirstOrDefault();
            if (cronicos != null) {
                var ultimo = _context.tblClientes.Select(x => new
                {
                    id = x.ClientesID,
                    CodigoYRazon = x.Nombres,
                    x.ApellidoPaterno,
                    x.ApellidoMaterno
                }).OrderBy(x => x.id);

                ViewBag.ClientesID = new SelectList(ultimo, "id", "CodigoYRazon");
                return View(cronicos);
            }
            else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarCronicos(Cronicos cronicos)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblCronicos.Where(S => S.GrupoFamiliar.Equals(cronicos.GrupoFamiliar)).FirstOrDefault();

                Editar.NCronicos = cronicos.NCronicos;
                Editar.NEmbarazadas = cronicos.NEmbarazadas;
                Editar.RSHID = cronicos.RSHID;
                Editar.TramoID = cronicos.TramoID;
                Editar.ObservacionGrupo = cronicos.ObservacionGrupo;
                Editar.GrupoFamiliar = cronicos.GrupoFamiliar;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");
            }
            else {
                return View();
            }
        }


        //ELIMINAR CLIENTES Y TODAS SUS CARACTERISTICAS
        [HttpGet]
        public IActionResult EliminarClientes(int ClientesID)
        {
            Datos();
            var Cliente = _context.tblClientes.Where(C => C.ClientesID.Equals(ClientesID)).FirstOrDefault();
            if (Cliente != null) {
                return View(Cliente);
            }
            else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarClientes(Clientes clientes)
        {
            Datos();
            if (ModelState.IsValid) {
                var ClienteEliminado = _context.tblClientes.Where(C => C.ClientesID.Equals(clientes.ClientesID)).FirstOrDefault();
                _context.Attach(ClienteEliminado).State = EntityState.Deleted;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexClientes));
            }
            else {
                return View(clientes);
            }
        }
    }
}
