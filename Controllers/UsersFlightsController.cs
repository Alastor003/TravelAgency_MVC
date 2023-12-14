using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency_MVC.Models;
using static TravelAgency_MVC.Controllers.UsersController;

namespace TravelAgency_MVC.Controllers
{
    public class UsersFlightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersFlightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UsersFlights
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.usersFlights.Include(u => u.flight).Include(u => u.user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UsersFlights/Details/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.usersFlights == null)
            {
                return NotFound();
            }

            var usersFlights = await _context.usersFlights
                .Include(u => u.flight)
                .Include(u => u.user)
                .FirstOrDefaultAsync(m => m.idUser == id);
            if (usersFlights == null)
            {
                return NotFound();
            }

            return View(usersFlights);
        }

        // GET: UsersFlights/Create
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult Create()
        {
            ViewData["idFlight"] = new SelectList(_context.flights, "id", "aircraft");
            ViewData["idUser"] = new SelectList(_context.users, "idUser", "email");
            return View();
        }

        // POST: UsersFlights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idUser,idFlight")] UsersFlights usersFlights)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersFlights);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idFlight"] = new SelectList(_context.flights, "id", "aircraft", usersFlights.idFlight);
            ViewData["idUser"] = new SelectList(_context.users, "idUser", "email", usersFlights.idUser);
            return View(usersFlights);
        }

        // GET: UsersFlights/Edit/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.usersFlights == null)
            {
                return NotFound();
            }

            var usersFlights = await _context.usersFlights.FindAsync(id);
            if (usersFlights == null)
            {
                return NotFound();
            }
            ViewData["idFlight"] = new SelectList(_context.flights, "id", "aircraft", usersFlights.idFlight);
            ViewData["idUser"] = new SelectList(_context.users, "idUser", "email", usersFlights.idUser);
            return View(usersFlights);
        }

        // POST: UsersFlights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idUser,idFlight")] UsersFlights usersFlights)
        {
            if (id != usersFlights.idUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersFlights);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersFlightsExists(usersFlights.idUser))
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
            ViewData["idFlight"] = new SelectList(_context.flights, "id", "aircraft", usersFlights.idFlight);
            ViewData["idUser"] = new SelectList(_context.users, "idUser", "email", usersFlights.idUser);
            return View(usersFlights);
        }

        // GET: UsersFlights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.usersFlights == null)
            {
                return NotFound();
            }

            var usersFlights = await _context.usersFlights
                .Include(u => u.flight)
                .Include(u => u.user)
                .FirstOrDefaultAsync(m => m.idUser == id);
            if (usersFlights == null)
            {
                return NotFound();
            }

            return View(usersFlights);
        }

        // POST: UsersFlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usersFlights == null)
            {
                return Problem("Entity set 'ApplicationDbContext.usersFlights'  is null.");
            }
            var usersFlights = await _context.usersFlights.FindAsync(id);
            if (usersFlights != null)
            {
                _context.usersFlights.Remove(usersFlights);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersFlightsExists(int id)
        {
          return (_context.usersFlights?.Any(e => e.idUser == id)).GetValueOrDefault();
        }
    }
}
