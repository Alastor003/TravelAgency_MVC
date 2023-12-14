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
    public class FlightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Flights
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.flights.Include(f => f.destination).Include(f => f.origin);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Flights/Details/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.flights == null)
            {
                return NotFound();
            }

            var flight = await _context.flights
                .Include(f => f.destination)
                .Include(f => f.origin)
                .FirstOrDefaultAsync(m => m.id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult Create()
        {
            ViewData["destinationId"] = new SelectList(_context.cities, "id", "id");
            ViewData["originId"] = new SelectList(_context.cities, "id", "id");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,originId,destinationId,soldFlights,capacity,flightPrice,date,airline,aircraft")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["destinationId"] = new SelectList(_context.cities, "id", "id", flight.destinationId);
            ViewData["originId"] = new SelectList(_context.cities, "id", "id", flight.originId);
            return View(flight);
        }

        // GET: Flights/Edit/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.flights == null)
            {
                return NotFound();
            }

            var flight = await _context.flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["destinationId"] = new SelectList(_context.cities, "id", "id", flight.destinationId);
            ViewData["originId"] = new SelectList(_context.cities, "id", "id", flight.originId);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,originId,destinationId,soldFlights,capacity,flightPrice,date,airline,aircraft")] Flight flight)
        {
            if (id != flight.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.id))
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
            ViewData["destinationId"] = new SelectList(_context.cities, "id", "id", flight.destinationId);
            ViewData["originId"] = new SelectList(_context.cities, "id", "id", flight.originId);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.flights == null)
            {
                return NotFound();
            }

            var flight = await _context.flights
                .Include(f => f.destination)
                .Include(f => f.origin)
                .FirstOrDefaultAsync(m => m.id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.flights == null)
            {
                return Problem("Entity set 'ApplicationDbContext.flights'  is null.");
            }
            var flight = await _context.flights.FindAsync(id);
            if (flight != null)
            {
                _context.flights.Remove(flight);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
          return (_context.flights?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
