using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NM.Data;
using NM.Models;

namespace NM.Controllers
{
    public class TriviasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TriviasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trivias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trivia.ToListAsync());
        }

        // GET: Trivias/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Trivias/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Trivia.Where( j => j.TriviaQuestion.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Trivias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trivia = await _context.Trivia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trivia == null)
            {
                return NotFound();
            }

            return View(trivia);
        }

        // GET: Trivias/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trivias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TriviaQuestion,TriviaAnswer")] Trivia trivia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trivia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trivia);
        }

        // GET: Trivias/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trivia = await _context.Trivia.FindAsync(id);
            if (trivia == null)
            {
                return NotFound();
            }
            return View(trivia);
        }

        // POST: Trivias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TriviaQuestion,TriviaAnswer")] Trivia trivia)
        {
            if (id != trivia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trivia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TriviaExists(trivia.Id))
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
            return View(trivia);
        }

        // GET: Trivias/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trivia = await _context.Trivia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trivia == null)
            {
                return NotFound();
            }

            return View(trivia);
        }

        // POST: Trivias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trivia = await _context.Trivia.FindAsync(id);
            _context.Trivia.Remove(trivia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TriviaExists(int id)
        {
            return _context.Trivia.Any(e => e.Id == id);
        }
    }
}
