using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaTienda.Models;
using Microsoft.AspNetCore.Authorization;

namespace LaTienda.Controllers
{
    [Authorize]
    public class TallesController : Controller
    {
        private readonly Context _context;

        public TallesController(Context context)
        {
            _context = context;
        }

        // GET: Talles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Talles.ToListAsync());
        }

        // GET: Talles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talle = await _context.Talles
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (talle == null)
            {
                return NotFound();
            }

            return View(talle);
        }

        // GET: Talles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Talles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Descripcion")] Talle talle)
        {
            if (ModelState.IsValid)
            {
                talle.Codigo = Guid.NewGuid();
                _context.Add(talle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(talle);
        }

        // GET: Talles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talle = await _context.Talles.FindAsync(id);
            if (talle == null)
            {
                return NotFound();
            }
            return View(talle);
        }

        // POST: Talles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Codigo,Descripcion")] Talle talle)
        {
            if (id != talle.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(talle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TalleExists(talle.Codigo))
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
            return View(talle);
        }

        // GET: Talles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talle = await _context.Talles
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (talle == null)
            {
                return NotFound();
            }

            return View(talle);
        }

        // POST: Talles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var talle = await _context.Talles.FindAsync(id);
            _context.Talles.Remove(talle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TalleExists(Guid id)
        {
            return _context.Talles.Any(e => e.Codigo == id);
        }
    }
}
