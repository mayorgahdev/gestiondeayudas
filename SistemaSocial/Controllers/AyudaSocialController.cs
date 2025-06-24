using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using SistemaSocial.Models;
using SistemaSocial.ViewModel;
using SistemaSocial.ViewModel.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SistemaSocial.Controllers
{
    [Authorize(Policy = "PolicyAdmin")]
    public class AyudaSocialController : Controller
    {
        private readonly AppDbContext _context;

        public AyudaSocialController(AppDbContext context)
        {
            _context = context;
        }

        private void Datos()
        {
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Rut");
            ViewData["ClientesID"] = new SelectList(_context.tblClientes.ToList(), "ClientesID", "Pasaporte");
            ViewData["PrestacionesID"] = new SelectList(_context.tblPrestaciones.OrderByDescending(x => x.Nombre != "Otra.").ThenBy(x => x.Nombre).ToList(), "PrestacionesID", "Nombre");
            ViewData["TipoPeticionID"] = new SelectList(_context.tblTipoPeticion.ToList(), "TipoPeticionID", "Nombre");
            ViewData["PeticionID"] = new SelectList(_context.tblPeticion.ToList(), "PeticionID", "Nombre");
        }


        [HttpGet]
        public IActionResult TipoDeAyuda(string Busqueda, int Pagina)
        {
            Datos();
            var asvm = new AyudaSocialView();

            //PAGINADO
            if (Pagina == 0) {
                asvm.Paginas = 1;
            }else {
                asvm.Paginas = Pagina;
            }
            int Muestra = 15;
            int Cantidad = _context.tblClientes.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                asvm.CantidadPaginas = Cantidad;
            }else {
                asvm.CantidadPaginas = Cantidad + 1;
            }
            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;

            //BUSQUEDA
            var ayuda = from a in _context.tblAyudaSocial select a;

            if (!String.IsNullOrEmpty(Busqueda)) {
                ayuda = ayuda.Where(a =>
                   a.Prestaciones.Nombre.Contains(Busqueda)
                || a.Clientes.Rut.Contains(Busqueda));
            }

            int total = _context.tblAyudaSocial.ToList().Count;
            asvm.Cantidad = total;

            asvm.listaAyudas = ayuda
                .OrderByDescending(a => a.AyudaSocialID)
                .Skip((asvm.Paginas - 1) * Muestra)
                .Take(Muestra)
                .ToList();

            return View(asvm);
        }
        [HttpGet]
        public async Task<IActionResult> ListadoFechas(int Pagina, DateTime FechaInicio, DateTime FechaFin, RecuperarDatos R)
        {
            Datos();

            var asvm = new AyudaSocialView();

            //PAGINADO
            if (Pagina == 0) {
                asvm.Paginas = 1;
            }else {
                asvm.Paginas = Pagina;
            }
            int Muestra = 15;
            int Cantidad = _context.tblClientes.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                asvm.CantidadPaginas = Cantidad;
            }else {
                asvm.CantidadPaginas = Cantidad + 1;
            }
            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;


            //FILTRADO
            var ayuda = from a in _context.tblAyudaSocial
                            where a.FechaElaboracion.Date >= FechaInicio.Date && a.FechaElaboracion.Date <= FechaFin.Date
                        select a;

            R.FechaInicio = FechaInicio;
            R.FechaFin = FechaFin;

            asvm.listaAyudas =
                ayuda.OrderBy(a => a.FechaElaboracion)
                .ToList();

            return View(asvm);
        }
        [HttpGet]
        public IActionResult Imprimir_Filtro(DateTime FechaInicio, DateTime FechaFin)
        {
            Datos();
            var asvm = new AyudaSocialView();
            var ayuda = from a in _context.tblAyudaSocial
                            where a.FechaElaboracion.Date >= FechaInicio.Date && a.FechaElaboracion.Date <= FechaFin.Date
                        select a;

            asvm.listaAyudas = ayuda.OrderBy(x => x.FechaElaboracion).ToList();

            {
                asvm.FechaInicio = FechaInicio.Date;
                asvm.FechaFin = FechaFin.Date;
            };
            //return View(asvm);
            return new ViewAsPdf("Imprimir_Filtro", asvm);
        }



        public IActionResult Estadisticas(int Pagina)
        {
            Datos();
            var asvm = new AyudaSocialView();

            //PAGINADO
            if (Pagina == 0) {
                asvm.Paginas = 1;
            }else {
                asvm.Paginas = Pagina;
            }
            int Muestra = 15;
            int Cantidad = _context.tblClientes.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                asvm.CantidadPaginas = Cantidad;
            }else {
                asvm.CantidadPaginas = Cantidad + 1;
            }
            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;


            //FILTRADO
            var ayuda = from a in _context.tblAyudaSocial select a;

            asvm.listaAyudas =
                ayuda.OrderBy(a => a.FechaElaboracion)
                .ToList();

            return View(asvm);
        }
            /*ESTADISTICAS AYUDAS*/
            [HttpGet]
            public JsonResult ReporteAyudasMes()
            {
                Datos_Retornar obj_ayudas = new Datos_Retornar();
                List<RetornarAyudasMes> ListaAyudas = obj_ayudas.RetornarAyudasMes();

                return Json(ListaAyudas);
            }
            [HttpGet]
            public JsonResult ReporteAyudas3Meses()
            {
                Datos_Retornar obj_ayudas = new Datos_Retornar();
                List<RetornarAyudas3Meses> ListaAyudas = obj_ayudas.RetornarAyudas3Meses();

                return Json(ListaAyudas);
            }
            [HttpGet]
            public JsonResult ReporteAyudasAnual()
            {
                Datos_Retornar obj_ayudas = new Datos_Retornar();
                List<RetornarAyudasAnual> ListaAyudas = obj_ayudas.RetornarAyudasAnual();

                return Json(ListaAyudas);
            }
            
            /*ESTADISTICAS PRESTACIONES*/
            [HttpGet]
            public JsonResult ReportePrestacionesMes()
            {
                Datos_Retornar obj_prestaciones = new Datos_Retornar();
                List<RetornarPrestacionesMes> ListaPrestaciones = obj_prestaciones.RetornarPrestacionesMes();

                return Json(ListaPrestaciones);
            }
            [HttpGet]
            public JsonResult ReportePrestaciones3Meses()
            {
                Datos_Retornar obj_prestaciones = new Datos_Retornar();
                List<RetornarPrestaciones3Meses> ListaPrestaciones = obj_prestaciones.RetornarPrestaciones3Meses();

                return Json(ListaPrestaciones);
            }
            [HttpGet]
            public JsonResult ReportePrestacionesAnual()
            {
                Datos_Retornar obj_prestaciones = new Datos_Retornar();
                List<RetornarPrestacionesAnual> ListaPrestaciones = obj_prestaciones.RetornarPrestacionesAnual();

                return Json(ListaPrestaciones);
            }

        public class RecuperarDatos
        {
            public DateTime FechaInicio { get; set; }
            public DateTime FechaFin { get; set; }
        }

        public IActionResult Estadisticas2(DateTime FechaInicio, DateTime FechaFin, RecuperarDatos R)
        {
            Datos();
            var asvm = new AyudaSocialView();
            List<DetalleRetornarAyudas> listaAyudas = new List<DetalleRetornarAyudas>();

            var ayuda = from a in _context.tblAyudaSocial select a;

            asvm.listaAyudas =
                ayuda.OrderBy(a => a.FechaElaboracion)
                .ToList();

            {
                asvm.FechaInicio = FechaInicio.Date;
                asvm.FechaFin = FechaFin.Date;
            };

            R.FechaInicio = FechaInicio;
            R.FechaFin = FechaFin;

            return View(asvm);
        }
            /* ESTADISTICAS POR FILTROS */
            public JsonResult EstadisticasAyudas(DateTime FechaInicio, DateTime FechaFin, RecuperarDatos R)
            {
                Datos();
                List<DetalleRetornarAyudas> listaAyudas = new List<DetalleRetornarAyudas>();
                var asvm = new AyudaSocialView();

                var ayuda = from a in _context.tblAyudaSocial select a;
                {
                    ViewBag.FechaInicio_1 = FechaInicio.Date;
                    ViewBag.FechaFin_1 = FechaFin.Date;
                };

                var RangoFechas = Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));

                if (FechaInicio == R.FechaInicio && FechaFin == R.FechaFin) {
                using (SqlConnection _context = new SqlConnection
                        ("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BDSocial; Integrated Security = True;")) {
                        //("Data Source = DESKTOP-FB4H9LQ\\SQLEXPRESS; Initial Catalog = BDSocial; Integrated Security = True;")) {
                        string query = "ObtenerEstadisticasAyudasEntregadas";
                        SqlCommand cmd = new SqlCommand(query, _context);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                       
                        _context.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader()) {
                            while (dr.Read()) {
                                listaAyudas.Add(new DetalleRetornarAyudas()
                                {
                                    fecha = dr["fecha"].ToString(),
                                    totalAyudas = int.Parse(dr["totalAyudas"].ToString()),
                                });
                            }
                        }
                    }
                    return Json(listaAyudas);
                }else {
                    return Json(listaAyudas);
                }
            }
            public JsonResult EstadisticasPres(DateTime FechaInicio, DateTime FechaFin, RecuperarDatos R)
            {
                Datos();
                List<DetalleRetornarPrestaciones> listaPrestaciones = new List<DetalleRetornarPrestaciones>();
                var asvm = new AyudaSocialView();

                var ayuda = from a in _context.tblAyudaSocial select a;
                {
                    ViewBag.FechaInicio_1 = FechaInicio.Date;
                    ViewBag.FechaFin_1 = FechaFin.Date;
                };

                if (FechaInicio == R.FechaInicio && FechaFin == R.FechaFin) {
                using (SqlConnection _context = new SqlConnection
                    ("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BDSocial; Integrated Security = True;")) {
                    //("Data Source = DESKTOP-FB4H9LQ\\SQLEXPRESS; Initial Catalog = BDSocial; Integrated Security = True;")) {
                    string query = "ObtenerEstadisticasPrestaciones";
                        SqlCommand cmd = new SqlCommand(query, _context);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);

                        _context.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader()) {
                            while (dr.Read()) {
                                listaPrestaciones.Add(new DetalleRetornarPrestaciones()
                                {
                                    Nombre = dr["Nombre"].ToString(),
                                    Cantidad = int.Parse(dr["Cantidad"].ToString()),
                                });
                            }
                        }
                    }
                    return Json(listaPrestaciones);
                }else {
                    return Json(listaPrestaciones);
                }
            }



        //CREAR AYUDA SOCIAL
        [HttpGet]
        public IActionResult CrearAyudaSocial(int ClientesID, int Grupo, string Tipo, int NumInforme) {                            
            
            Datos();
            ViewBag.ClientesID1 = ClientesID;
            ViewBag.NumInforme = NumInforme;
            ViewBag.Tipo = Tipo;
            ViewBag.GrupoA = Grupo;
            
            ViewBag.ClientesID = new SelectList(_context.tblAyudaSocial.Where(x => x.Clientes.ClientesID.Equals(ClientesID)));

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

            AyudaSocial ayuda = new AyudaSocial();
            string fecha = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            ayuda.FechaElaboracion = DateTime.ParseExact(fecha, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

            return View(ayuda);
        }
            [HttpGet]
            public JsonResult LoadPrestaciones() {
                var prestaciones = _context.tblPrestaciones.ToList();
                return Json(prestaciones);
            }
            [HttpGet]
            public JsonResult GetPrestaciones(int PrestacionesID) {
                var prestaciones = _context.tblPrestaciones.Where(c => c.PrestacionesID == PrestacionesID).ToList();
                return Json(prestaciones);
            }
        [HttpPost]
        public async Task<IActionResult> CrearAyudaSocial(AyudaSocial Ayuda, int ClientesID, int Grupo, string Tipo) {
            Datos();
            ViewBag.ClientesID1 = ClientesID;
            ViewBag.Tipo = Tipo;
            ViewBag.PeticionTexto = _context.tblPeticion.Select(p => p.Nombre).First();
            ViewBag.PeticionTexto = Regex.Replace(ViewBag.PeticionTexto, @"\.(s)+", ".");

            ViewBag.ClientesID = new SelectList(_context.tblAyudaSocial.Where(x => x.Clientes.ClientesID.Equals(ClientesID)));
            
            var Clientes = _context.tblClientes.Select(x => new
            {
                id = x.ClientesID,
                nombre = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno

            }).OrderByDescending(x => x.id);
            ViewBag.ClientesID = new SelectList(Clientes, "id", "nombre");

            if (ModelState.IsValid) {
                foreach (var A in Ayuda.MultiPrestaciones) {
                    AyudaSocial ayudaSocial = new AyudaSocial();

                    ayudaSocial.ClientesID = Ayuda.ClientesID;
                    ayudaSocial.NumInforme = Ayuda.NumInforme;
                    ayudaSocial.TipoPeticionID = Ayuda.TipoPeticionID;
                    ayudaSocial.PrestacionesID = int.Parse(A);
                    ayudaSocial.CantidadEntregada = Ayuda.CantidadEntregada;
                    ayudaSocial.StockBodega = Ayuda.StockBodega;
                    ayudaSocial.DetalleRequerimiento = Ayuda.DetalleRequerimiento;
                    ayudaSocial.FechaElaboracion = Ayuda.FechaElaboracion;
                    ayudaSocial.NumAyuda = Ayuda.NumAyuda;
                    ayudaSocial.GrupoFamiliar = Ayuda.GrupoFamiliar;

                    _context.tblAyudaSocial.Add(ayudaSocial);
                    _context.SaveChanges();
                    Ayuda.NumAyuda = ayudaSocial.NumAyuda;
                    Ayuda.GrupoFamiliar = ayudaSocial.GrupoFamiliar;
                }
                if (bool.Parse(Ayuda.StockBodega) == true) {
                    return RedirectToAction("CrearInformeSocial", "InformeSocial",
                        new 
                        {
                            ClientesID = Ayuda.ClientesID,
                            Grupo = Ayuda.GrupoFamiliar,
                            NumInforme = Ayuda.NumInforme
                        });
                }
                else {
                    return RedirectToAction("CrearPeticionAyuda", "AyudaSocial",
                        new
                        {
                            ClientesID = Ayuda.ClientesID,
                            Grupo = Ayuda.GrupoFamiliar,
                            NumInforme = Ayuda.NumInforme
                        });
                }
            }else {
                return View(Ayuda);
            }
        }


        //CREAR PETICION PARA AYUDA SOCIAL
        [HttpGet]
        public IActionResult CrearPeticionAyuda(int ClientesID, int NumInforme, int Grupo)
        {
            Datos();
            ViewBag.GrupoP = Grupo;
            ViewBag.NumInforme = NumInforme;
            ViewBag.PeticionTexto = _context.tblPeticion.Select(p => p.Nombre).First();

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
        public async Task<IActionResult> CrearPeticionAyuda(PeticionAyuda peticion, int ClientesID)
        {
            ViewBag.ClientesID = new SelectList(_context.tblPeticionAyuda.Where(x => x.Clientes.ClientesID.Equals(ClientesID)));

            var Clientes = _context.tblClientes.Select(x => new
            {
                id = x.ClientesID,
                nombre = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno

            }).OrderByDescending(x => x.id);
            ViewBag.ClientesID = new SelectList(Clientes, "id", "nombre");

            if (ModelState.IsValid) {
                _context.Add(peticion);
                _context.SaveChanges();
                return RedirectToAction("CrearMedioAyuda", "MedioAyuda",
                        new
                        {
                            ClientesID = peticion.ClientesID,
                            Grupo = peticion.GrupoFamiliar,
                            NumInforme = peticion.NumInforme
                        });
            }
            else {
                return View(peticion);
            }
        }



        //EDITAR AYUDA SOCIAL
        [HttpGet]
        public IActionResult EditarAyudaSocial(int AyudaID, int ClientesID, int NumInforme)
        {
            Datos();
            var Ayudas = _context.tblAyudaSocial
                .Where(x => x.AyudaSocialID.Equals(AyudaID))
                .Where(x => x.ClientesID.Equals(ClientesID))
                .Where(x => x.NumInforme.Equals(NumInforme))
                .FirstOrDefault();

            if (Ayudas != null) {
                var ultimo = _context.tblClientes.Select(x => new
                {
                    id = x.ClientesID,
                    CodigoYRazon = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno
                }).OrderBy(x => x.id);

                ViewBag.ClientesID = new SelectList(ultimo, "id", "CodigoYRazon");

                return View(Ayudas);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarAyudaSocial(AyudaSocial Ayuda)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblAyudaSocial
                    .Where(x => x.AyudaSocialID.Equals(Ayuda.AyudaSocialID))
                    .Where(x => x.ClientesID.Equals(Ayuda.ClientesID))
                    .Where(x => x.NumInforme.Equals(Ayuda.NumInforme))
                    .FirstOrDefault();

                Editar.ClientesID = Ayuda.ClientesID;
                Editar.TipoPeticionID = Ayuda.TipoPeticionID;
                Editar.PrestacionesID = Ayuda.PrestacionesID;
                Editar.CantidadEntregada = Ayuda.CantidadEntregada;
                Editar.StockBodega = Ayuda.StockBodega;
                Editar.DetalleRequerimiento = Ayuda.DetalleRequerimiento;
                Editar.FechaElaboracion = Ayuda.FechaElaboracion;
                Editar.NumAyuda = Ayuda.NumAyuda;
                Editar.NumInforme = Ayuda.NumInforme;
                Editar.GrupoFamiliar = Ayuda.GrupoFamiliar;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");
            }else {
                return View(Ayuda);
            }
        }


        //EDITAR PETICION AYUDA
        [HttpGet]
        public IActionResult EditarPeticionAyuda(int PeticionAyudaID, int ClientesID, int NumInforme)
        {
            Datos();
            var peticion = _context.tblPeticionAyuda
                .Where(x => x.PeticionAyudaID.Equals(PeticionAyudaID))
                .Where(x => x.ClientesID.Equals(ClientesID))
                .Where(x => x.NumInforme.Equals(NumInforme))
                .FirstOrDefault();

            if (peticion != null) {
                var ultimo = _context.tblClientes.Select(x => new
                {
                    id = x.ClientesID,
                    CodigoYRazon = x.Nombres, x.ApellidoPaterno, x.ApellidoMaterno
                }).OrderBy(x => x.id);

                ViewBag.ClientesID = new SelectList(ultimo, "id", "CodigoYRazon");

                return View(peticion);
            }
            else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarPeticionAyuda(PeticionAyuda peticion)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblPeticionAyuda
                    .Where(a => a.PeticionAyudaID.Equals(peticion.PeticionAyudaID))
                    .Where(x => x.ClientesID.Equals(peticion.ClientesID))
                    .Where(x => x.NumInforme.Equals(peticion.NumInforme))
                    .FirstOrDefault();

                Editar.ClientesID = peticion.ClientesID;
                Editar.PeticionTexto = peticion.PeticionTexto;
                Editar.NumInforme = peticion.NumInforme;
                Editar.GrupoFamiliar = peticion.GrupoFamiliar;

                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");
            }
            else {
                return View(peticion);
            }
        }


        //EDITAR PETICIÓN
        [HttpGet]
        public IActionResult EditarPeticion(int PeticionID)
        {
            Datos();
            ViewBag.PeticionID = 1;
            var Ayudas = _context.tblPeticion
                .Where(S => S.PeticionID.Equals(PeticionID))
                .FirstOrDefault();

            if (Ayudas != null) {
                return View(Ayudas);
            }else {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarPeticion(Peticion peticion)
        {
            Datos();
            if (ModelState.IsValid) {
                var Editar = _context.tblPeticion.Where(a => a.PeticionID.Equals(peticion.PeticionID)).FirstOrDefault();
                Editar.Nombre = peticion.Nombre;
                _context.SaveChanges();
                return RedirectToAction("IndexClientes", "Clientes");
            }else {
                return View(peticion);
            }
        }



        //ELIMINAR AYUDA SOCIAL
        [HttpGet]
        public IActionResult EliminarAyudaSocial(int AyudaID)
        {
            Datos();
            var Cliente = _context.tblAyudaSocial.Where(A => A.AyudaSocialID.Equals(AyudaID)).FirstOrDefault();
            if (Cliente != null) {
                return View(Cliente);
            }else {
                return NotFound();
            }

        }
        [HttpPost]
        public async Task<IActionResult> EliminarAyudaSocial(AyudaSocial ayudaSocial)
        {
            Datos();
            if (ModelState.IsValid) {
                var AyudaEliminada = _context.tblAyudaSocial.Where(A => A.AyudaSocialID.Equals(ayudaSocial.AyudaSocialID)).FirstOrDefault();
                _context.Attach(AyudaEliminada).State = EntityState.Deleted;
                _context.SaveChanges();

                return RedirectToAction(nameof(TipoDeAyuda));
            }else {
                return View(ayudaSocial);
            }
        }
    }
}
