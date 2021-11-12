﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineORT.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CineORT.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly CineDbContext _context;

        private const string _Return_Url = "ReturnUrl";
        public UsuariosController(CineDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        [HttpGet]
        public IActionResult LoginUsuario(string returnUrl)
        {
            TempData[_Return_Url] = returnUrl;
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginUsuario(string Email, string Contrasenia)
        {
            string returnUrl = TempData[_Return_Url] as string;

            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Contrasenia))
            {
                // if (!ValidarUsuario(usuario))
                // {
                //    ViewBag.Error = "Usuario Inexistente";
                //  }

                //var usuario = ObtenerUsuario(_context, usuario.Email, usuario.Contrasenia);
                //Usuario usuario1 = ObtenerUsuario(_context, email: usuario1.Email, usuario1.Contrasenia);

                Usuario usuario = null;

                usuario = _context.Usuarios.FirstOrDefault(o => o.Email == Email && Contrasenia == o.Contrasenia);

                //usuario = _context.Usuarios.Find(_context, Email, Contrasenia);

                if (usuario != null && ValidarUsuario(usuario))
                {
                    
                    // Se crean las credenciales del usuario que serán incorporadas al contexto
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    // El lo que luego obtendré al acceder a User.Identity.Name
                    identity.AddClaim(new Claim(ClaimTypes.Name, usuario.NombreApellido));

                    // Se utilizará para la autorización por roles
                    identity.AddClaim(new Claim(ClaimTypes.Role, usuario.ToString()));

                    // Lo utilizaremos para acceder al Id del usuario que se encuentra en el sistema.
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                    // Lo utilizaremos cuando querramos mostrar el nombre del usuario logueado en el sistema.
                    identity.AddClaim(new Claim(ClaimTypes.GivenName, usuario.NombreApellido));

                    //identity.AddClaim(new Claim(nameof(Usuario.Foto), usuario.Foto ?? string.Empty));

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    // En este paso se hace el login del usuario al sistema
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                    TempData["LoggedIn"] = true;

                    return Redirect(returnUrl);


                }
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            ViewBag.Email = Email;
            TempData[_Return_Url] = returnUrl;
            return View();
        }


        [Authorize]
             public IActionResult Logout()
          {
        
              return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        private bool ValidarUsuario(Usuario usuario)
        {
            var listaUsuarios = _context.Usuarios.ToList();
            bool encontrado = listaUsuarios
                .Where(a => a.Email != null)
                .Any(usu => usu.Email.Equals(usuario.Email, System.StringComparison.OrdinalIgnoreCase) && usu.Id != usuario.Id);

            return encontrado;
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                
                if (!ValidarUsuario(usuario))
                {
                    
                        _context.Usuarios.Add(usuario);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    
                    
                    
                }
                else
                {
                    ViewBag.Error = "Usuario Existente";
                }
                
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            

            if (ModelState.IsValid)
            {
                if (!ValidarUsuario(usuario))
                {
                try
                {
                        var usuarioBD = _context.Usuarios.FirstOrDefault(o => o.Id == usuario.Id);
                        usuarioBD.Email = usuario.Email;
                    _context.Update(usuarioBD);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {


                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.Error = "Usuario Existente";
                    return View(usuario);
                }
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
