using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using U2_Prueba1.Models;

namespace U2_Prueba1.Controllers
{
    public class AsignaturasEstudiantesController : Controller
    {
        private readonly DbU2Context _context;

        public AsignaturasEstudiantesController(DbU2Context context)
        {
            _context = context;
        }

        // GET: AsignaturasEstudiantes
        public async Task<IActionResult> Index()
        {
            var dbU2Context = _context.AsignaturasEstudiantes.Include(a => a.Asignaturas).Include(a => a.Estudiantes);
            return View(await dbU2Context.ToListAsync());
        }

        // GET: AsignaturasEstudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AsignaturasEstudiantes == null)
            {
                return NotFound();
            }

            var asignaturasEstudiante = await _context.AsignaturasEstudiantes
                .Include(a => a.Asignaturas)
                .Include(a => a.Estudiantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturasEstudiante == null)
            {
                return NotFound();
            }

            return View(asignaturasEstudiante);
        }

        // GET: AsignaturasEstudiantes/Create
        public IActionResult Create()
        {
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: AsignaturasEstudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EstudiantesId,AsignaturasId,FechaRegistro")] AsignaturasEstudiante asignaturasEstudiante)
        {
            if (asignaturasEstudiante.EstudiantesId != 0 && asignaturasEstudiante.AsignaturasId !=0)
            {
                _context.Add(asignaturasEstudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturasEstudiante.AsignaturasId);
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturasEstudiante.EstudiantesId);
            return View(asignaturasEstudiante);
        }

        // GET: AsignaturasEstudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AsignaturasEstudiantes == null)
            {
                return NotFound();
            }

            var asignaturasEstudiante = await _context.AsignaturasEstudiantes.FindAsync(id);
            if (asignaturasEstudiante == null)
            {
                return NotFound();
            }
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturasEstudiante.AsignaturasId);
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturasEstudiante.EstudiantesId);
            return View(asignaturasEstudiante);
        }

        // POST: AsignaturasEstudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstudiantesId,AsignaturasId,FechaRegistro")] AsignaturasEstudiante asignaturasEstudiante)
        {
            if (id != asignaturasEstudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignaturasEstudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturasEstudianteExists(asignaturasEstudiante.Id))
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
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturasEstudiante.AsignaturasId);
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturasEstudiante.EstudiantesId);
            return View(asignaturasEstudiante);
        }

        // GET: AsignaturasEstudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AsignaturasEstudiantes == null)
            {
                return NotFound();
            }

            var asignaturasEstudiante = await _context.AsignaturasEstudiantes
                .Include(a => a.Asignaturas)
                .Include(a => a.Estudiantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturasEstudiante == null)
            {
                return NotFound();
            }

            return View(asignaturasEstudiante);
        }

        // POST: AsignaturasEstudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AsignaturasEstudiantes == null)
            {
                return Problem("Entity set 'DbU2Context.AsignaturasEstudiantes'  is null.");
            }
            var asignaturasEstudiante = await _context.AsignaturasEstudiantes.FindAsync(id);
            if (asignaturasEstudiante != null)
            {
                _context.AsignaturasEstudiantes.Remove(asignaturasEstudiante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignaturasEstudianteExists(int id)
        {
          return (_context.AsignaturasEstudiantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
