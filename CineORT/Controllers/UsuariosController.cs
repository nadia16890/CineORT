using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineORT.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CineORT.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly CineDbContext _context;

        public UsuariosController(CineDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuario.ToListAsync());
        }

        public IActionResult LoginUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                if (!ValidarUsuario(usuario))
                {
                    ViewBag.Error = "Usuario Inexistente";
                }

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

                return RedirectToAction(nameof(ReservasController.Create), "Reservar");
            }
            return View(usuario);
        }
        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
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
            var listaUsuarios = _context.Usuario.ToList();
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
                    
                        _context.Usuario.Add(usuario);
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

            var usuario = await _context.Usuario.FindAsync(id);
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
                        var usuarioBD = _context.Usuario.FirstOrDefault(o => o.Id == usuario.Id);
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

            var usuario = await _context.Usuario
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
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
