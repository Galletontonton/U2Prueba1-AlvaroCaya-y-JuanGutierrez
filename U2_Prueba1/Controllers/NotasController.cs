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
    public class NotasController : Controller
    {
        private readonly DbU2Context _context;

        public NotasController(DbU2Context context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var dbU2Context = _context.Notas.Include(n => n.Asignaturas).Include(n => n.Estudiantes);
            return View(await dbU2Context.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Asignaturas)
                .Include(n => n.Estudiantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nota1,Ponderacion,EstudiantesId,AsignaturasId")] Nota nota)
        {
            if (nota.Nota1 != 0 && nota.Ponderacion != 0)
            {
                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", nota.AsignaturasId);
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id", nota.EstudiantesId);
            return View(nota);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", nota.AsignaturasId);
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id", nota.EstudiantesId);
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nota1,Ponderacion,EstudiantesId,AsignaturasId")] Nota nota)
        {
            if (id != nota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.Id))
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
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", nota.AsignaturasId);
            ViewData["EstudiantesId"] = new SelectList(_context.Estudiantes, "Id", "Id", nota.EstudiantesId);
            return View(nota);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Asignaturas)
                .Include(n => n.Estudiantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notas == null)
            {
                return Problem("Entity set 'DbU2Context.Notas'  is null.");
            }
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
          return (_context.Notas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
