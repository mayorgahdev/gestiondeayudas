using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SistemaSocial.Models;
using SistemaSocial.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SistemaWebSocial.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly AppDbContext _context;
        private readonly ILogger<Usuario> _log;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AccountController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, AppDbContext context, ILogger<Usuario> log, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _log = log;
            _hostEnvironment = hostEnvironment;
        }
       
        private void Datos()
        {
            ViewData["TipoUsuarioID"] = new SelectList(_context.tblTipoUsuario.ToList(), "TipoUsuarioID", "Nombre");
            ViewData["ProfesionID"] = new SelectList(_context.tblProfesion.ToList(), "ProfesionID", "Nombre");
        }

        public IActionResult IndexUsuario(int Pagina)
        {
            Datos();
            var user = new UsuarioView();

            if (Pagina == 0) {
                user.Paginas = 1;
            }else {
                user.Paginas = Pagina;
            }

            int Muestra = 6;
            int Cantidad = _context.Users.ToList().Count / Muestra;
            if (Cantidad % Muestra == 0) {
                user.CantidadPaginas = Cantidad;
            }else {
                user.CantidadPaginas = Cantidad + 1;
            }

            user.listaUsuario = _context
                .Users.Skip((user.Paginas - 1) * Muestra)
                .OrderBy(x => x.Rut)
                .Take(Muestra).ToList();

            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;

            return View(user);
        }


        //EDITAR - AGREGAR VISTA
        [Authorize(Policy = "PolicyAdmin")]
        [HttpGet]
        public IActionResult EditarUsuario(string UsuarioID)
        {
            Datos();
            var Usuario = _context.Users.Where(U => U.Id.Equals(UsuarioID)).FirstOrDefault();
            if (Usuario != null) {
                return View(Usuario);
            }else {
                return NotFound();
            }
        }
        [Authorize(Policy = "PolicyAdmin")]
        [HttpPost]
        public async Task<IActionResult> EditarUsuario(Usuario usuario)
        {
            Datos();
            if (ModelState.IsValid) {
                var EditUser = _context.Users.Where(U => U.Id.Equals(usuario.Id)).FirstOrDefault();

                EditUser.Rut = usuario.Rut;
                EditUser.Nombre = usuario.Nombre;
                EditUser.Apellido = usuario.Apellido;
                EditUser.Email = usuario.Email;
                EditUser.UserName = usuario.UserName;
                EditUser.ProfesionID = usuario.ProfesionID;
                EditUser.PhoneNumber = usuario.PhoneNumber;
                EditUser.Marca = usuario.Marca;
                EditUser.Firma = usuario.Firma;

                _context.SaveChanges();
                return RedirectToAction(nameof(IndexUsuario));
            }else {
                return View();
            }
        }


        //ELIMINAR - AGREGAR VISTA
        [Authorize(Policy = "PolicyAdmin")]
        [HttpGet]
        public IActionResult EliminarUsuario(string UsuarioID)
        {
            Datos();
            var Usuario = _context.Users.Where(C => C.Id.Equals(UsuarioID)).FirstOrDefault();
            if (Usuario != null) {
                return View(Usuario);
            }else {
                return NotFound();
            }
        }
        [Authorize(Policy = "PolicyAdmin")]
        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(Usuario usuario)
        {
            Datos();
            if (!ModelState.IsValid) {
                var Eliminar = _context.Users.Where(C => C.Id.Equals(usuario.Id)).FirstOrDefault();
                _context.Attach(Eliminar).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();

                return RedirectToAction(nameof(IndexUsuario));
            }else {
                return View(usuario);
            }
        }


        //LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid) {
                var User = await _userManager.FindByEmailAsync(lvm.Correo);

                if (User != null) {
                    var result = await _signInManager.PasswordSignInAsync(User, lvm.Contraseña, false, false);
                    if (result.Succeeded) {
                        return RedirectToAction("IndexClientes", "Clientes");
                    }else {
                        ModelState.AddModelError("", "Contraseña Incorrecta, Intente Nuevamente");
                    }
                }else {
                    ModelState.AddModelError("", "Correo Incorrecto, Intente Nuevamente");
                }
            }else {
                return View(lvm);
            }
            return View(lvm);
        }


        //REGISTRO
        [Authorize(Policy = "PolicyAdmin")]
        [HttpGet]
        public IActionResult Registro()
        {
            Datos();
            return View();
        }
        [Authorize(Policy = "PolicyAdmin")]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroView rvm)
        {
            Datos();
            if (ModelState.IsValid) {
                if (rvm.archivoImagen != null) {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(rvm.archivoImagen.FileName); // sinfoto
                    string extension = Path.GetExtension(rvm.archivoImagen.FileName); // .png
                    rvm.Imagen = fileName + DateTime.Now.ToString("ddMMMMyyyyHHmmss") + extension; // sinfoto(fecha + hora).png
                    string path = Path.Combine(wwwRootPath + "/images/" + rvm.Imagen);

                    using (var fileStream = new FileStream(path, FileMode.Create)) {
                        await rvm.archivoImagen.CopyToAsync(fileStream);
                    }
                }else {
                    //desde el form no se cargo una imagen
                    rvm.Imagen = "sin-foto.png";
                }

                var User = new Usuario() {
                    Rut = rvm.Rut,
                    Nombre = rvm.Nombre,
                    Apellido = rvm.Apellido,
                    TipoUsuarioID = rvm.TipoUsuarioID,
                    Email = rvm.Correo,
                    ProfesionID = rvm.ProfesionID,
                    UserName = rvm.Usuario,
                    archivoImagen = rvm.archivoImagen,
                    Imagen = rvm.Imagen,
                    Marca = rvm.Marca,
                    PhoneNumber = rvm.Telefono,
                    Firma = rvm.Firma
                };

                var result = await _userManager.CreateAsync(User, rvm.Contraseña);

                if (result.Succeeded) {
                    if (User.TipoUsuarioID == 1) {
                        await _userManager.AddClaimAsync(User, new System.Security.Claims.Claim("Admin", "10"));
                        return RedirectToAction(nameof(IndexUsuario));
                    }else {
                        await _userManager.AddClaimAsync(User, new System.Security.Claims.Claim("Admin", "5"));
                        return RedirectToAction(nameof(IndexUsuario));
                    }
                }else {
                    ModelState.AddModelError("", "Datos incorrectos, intente nuevamente. " +
                        "(Verifique que los datos ingresados no estén registrados ya en el sistema.)");
                }
            }
            return View(rvm);
        }






        //•ACCIONES PARA RECUPERAR CONTRASEÑA
        //•OLVIDO SU CONTRASEÑA
        [HttpGet]
        public IActionResult aOlvidoContraseña()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> aOlvidoContraseña(PasswordView modelo)
        {
            if (ModelState.IsValid)
            {
                var Usuario = await _userManager.FindByEmailAsync(modelo.Correo);
                if (Usuario != null || await _userManager.IsEmailConfirmedAsync(Usuario))
                {
                    var linkResetPass = "https://localhost:44313/Account/cReseteaPassword?Correo=" + modelo.Correo;

                    _log.Log(LogLevel.Warning, linkResetPass);
                    //SERVIDOR DE CORREO 
                    //SendGrid 

                    return View("bOlvidoPasswordConfirmacion");
                }
                return View("bOlvidoPasswordConfirmacion");
            }
            return View(modelo);
        }
        public IActionResult bOlvidoPasswordConfirmacion()
        {
            return View();
        }



        //RESETEAR CONTRASEÑA
        [HttpGet]
        public IActionResult cReseteaPassword(string Token, string Correo)
        {
            if (Token == null || Correo == null)
            {
                ModelState.AddModelError("", "Token incorrecto");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> cReseteaPassword(ResetPasswordView modelo)
        {
            if (ModelState.IsValid)
            {
                var Usuario = await _userManager.FindByEmailAsync(modelo.Correo);
                if (Usuario != null)
                {
                    var Resultado = await _userManager.ResetPasswordAsync(Usuario, modelo.Token, modelo.Contraseña);
                    if (Resultado.Succeeded)
                    {
                        return View("dReseteaPasswordConfirmacion");
                    }

                    foreach (var error in Resultado.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(modelo);
                }
                return View("dReseteaPasswordConfirmacion");
            }
            return View(modelo);
        }
        public IActionResult dReseteaPasswordConfirmacion() 
        {
            return View();
        }






        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
