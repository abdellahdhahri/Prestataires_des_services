using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServicesPlomberie.Data;
using ServicesPlomberie.Models;

namespace ServicesPlomberie.Controllers
{
    public class SpecialitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Specialites
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Specialite.Include(s => s.service);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Specialites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Specialite == null)
            {
                return NotFound();
            }

            var specialite = await _context.Specialite
                .Include(s => s.service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialite == null)
            {
                return NotFound();
            }

            return View(specialite);
        }

        // GET: Specialites/Create
        public IActionResult Create()
        {
            ViewData["serviceId"] = new SelectList(_context.Service, "id", "nom");
            return View();
        }

        // POST: Specialites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nom,code,serviceId")] Specialite specialite)
        {
           /* if (ModelState.IsValid)
            {*/
                _context.Add(specialite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           // }
            ViewData["serviceId"] = new SelectList(_context.Service, "id", "nom", specialite.serviceId);
            return View(specialite);
        }

        // GET: Specialites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Specialite == null)
            {
                return NotFound();
            }

            var specialite = await _context.Specialite.FindAsync(id);
            if (specialite == null)
            {
                return NotFound();
            }
            ViewData["serviceId"] = new SelectList(_context.Service, "id", "id", specialite.serviceId);
            return View(specialite);
        }

        // POST: Specialites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nom,code,serviceId")] Specialite specialite)
        {
            if (id != specialite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialiteExists(specialite.Id))
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
            ViewData["serviceId"] = new SelectList(_context.Service, "id", "id", specialite.serviceId);
            return View(specialite);
        }

        // GET: Specialites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Specialite == null)
            {
                return NotFound();
            }

            var specialite = await _context.Specialite
                .Include(s => s.service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialite == null)
            {
                return NotFound();
            }

            return View(specialite);
        }

        // POST: Specialites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Specialite == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Specialite'  is null.");
            }
            var specialite = await _context.Specialite.FindAsync(id);
            if (specialite != null)
            {
                _context.Specialite.Remove(specialite);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialiteExists(int id)
        {
          return (_context.Specialite?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
