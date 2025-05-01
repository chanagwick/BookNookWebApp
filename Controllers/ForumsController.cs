using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookNookWebApp.Data;
using BookNookWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookNookWebApp.Controllers
{
    public class ForumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Forums
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Forums.ToListAsync());
        }

        // GET: Forums/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forums = await _context.Forums
                .FirstOrDefaultAsync(m => m.ForumID == id);
            if (forums == null)
            {
                return NotFound();
            }

            return View(forums);
        }

        // GET: Forums/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Forums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForumID,Name,Description")] Forums forums)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forums);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forums);
        }

        // GET: Forums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forums = await _context.Forums.FindAsync(id);
            if (forums == null)
            {
                return NotFound();
            }
            return View(forums);
        }

        // POST: Forums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ForumID,Name,Description")] Forums forums)
        {
            if (id != forums.ForumID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forums);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumsExists(forums.ForumID))
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
            return View(forums);
        }

        // GET: Forums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forums = await _context.Forums
                .FirstOrDefaultAsync(m => m.ForumID == id);
            if (forums == null)
            {
                return NotFound();
            }

            return View(forums);
        }

        // POST: Forums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var forums = await _context.Forums.FindAsync(id);
            if (forums != null)
            {
                _context.Forums.Remove(forums);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumsExists(int id)
        {
            return _context.Forums.Any(e => e.ForumID == id);
        }
    }
}
