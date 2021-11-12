using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineORT.Models;

namespace CineORT.Controllers
{
    public class AdministradorsController : Controller
    {
        private readonly CineDbContext _context;

        public AdministradorsController(CineDbContext context)
        {
            _context = context;
        }

        public IActionResult MenuPrincipalAdministrador()
        {
            return View();
        }

        // GET: Administradors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Administrador.ToListAsync());
        }
        public IActionResult LoginAdministradors()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginAdministradors([Bind("Email,Contrasenia")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {

                if (!ValidarAdministradors(administrador))
                {
                    ViewBag.Error = "Administrador Inexistente";
                }

            }

            return RedirectToAction(nameof(AdministradorsController.MenuPrincipalAdministrador), "Menú Principal Administrador");
            //return View(administrador);

        }
            // GET: Administradors/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administrador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

            // GET: Administradors/Create
            public IActionResult Create()
        {
            return View();
        }
            private bool ValidarAdministradors(Administrador administrador)
            {
                var listaAdministrador = _context.Administrador.ToList();
                bool encontrado = listaAdministrador
                    .Where(a => a.Email != null)
                    .Any(admin => admin.Email.Equals(administrador.Email, System.StringComparison.OrdinalIgnoreCase) && admin.Id != administrador.Id);

                return encontrado;
            }

            // POST: Administradors/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Contrasenia")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                    if (!ValidarAdministradors(administrador))
                    {
                        _context.Add(administrador);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = "Administrador Existente";
                    }

                }
                return View(administrador);
            }


            // GET: Administradors/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administrador.FindAsync(id);
            if (administrador == null)
            {
                return NotFound();
            }
            return View(administrador);
        }

        // POST: Administradors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Contrasenia")] Administrador administrador)
        {
                if (ModelState.IsValid)
                {
                    if (!ValidarAdministradors(administrador))
                    {
                        try
                        {
                            var usuarioBD = _context.Administrador.FirstOrDefault(o => o.Id == administrador.Id);
                            usuarioBD.Email = administrador.Email;
                            _context.Update(usuarioBD);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!AdministradorExists(administrador.Id))
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
                        ViewBag.Error = "Administrador Existente";
                        return View(administrador);
                    }
                }
                return View(administrador);
            }


            // GET: Administradors/Delete/5
            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administrador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

        // POST: Administradors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrador = await _context.Administrador.FindAsync(id);
            _context.Administrador.Remove(administrador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

            private bool AdministradorExists(int id)
        {
            return _context.Administrador.Any(e => e.Id == id);
        }
    }
}
