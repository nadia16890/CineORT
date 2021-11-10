using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineORT.Models;
using Microsoft.AspNetCore.Authorization;

namespace CineORT.Controllers
{
   // [Authorize(TipoUsuario = nameof(TipoUsuario.Administrador))]
    public class FuncionsController : Controller
    {
        private readonly CineDbContext _context;

        

        public FuncionsController(CineDbContext context)
        {
            _context = context;
        }

        // GET: Funcions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcion.ToListAsync());
        }

        // GET: Funcions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // GET: Funcions/Create
        public IActionResult Create()
        {
            return View();
        }

        private bool ValidarPelicula(Funcion funcion)
        {
            var listaPeliculas = _context.Pelicula.ToList();
            bool encontrado = listaPeliculas
                .Where(a => a.Nombre != null)
                .Any(peli => peli.Nombre.Equals(funcion.NombrePelicula, System.StringComparison.OrdinalIgnoreCase));

            
            return encontrado;
        }
        // POST: Funcions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Horario,Sala,NombrePelicula")] Funcion funcion)
        {

            if (ModelState.IsValid)
            {
                if (ValidarPelicula(funcion))
                {
                   
                    _context.Add(funcion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Película Inexistente";
                }
                
            }
            return View(funcion);
        }

        // GET: Funcions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion.FindAsync(id);
            if (funcion == null)
            {
                return NotFound();
            }
            return View(funcion);
        }

        // POST: Funcions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Horario")] Funcion funcion)
        {
            if (id != funcion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionExists(funcion.Id))
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
            return View(funcion);
        }

        // GET: Funcions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // POST: Funcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcion = await _context.Funcion.FindAsync(id);
            _context.Funcion.Remove(funcion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionExists(int id)
        {
            return _context.Funcion.Any(e => e.Id == id);
        }
    }
}
