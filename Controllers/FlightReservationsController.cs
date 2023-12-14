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
    public class FlightReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FlightReservations
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.flightsReservation.Include(f => f.myFlight).Include(f => f.myUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FlightReservations/Details/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.flightsReservation == null)
            {
                return NotFound();
            }

            var flightReservation = await _context.flightsReservation
                .Include(f => f.myFlight)
                .Include(f => f.myUser)
                .FirstOrDefaultAsync(m => m.id == id);
            if (flightReservation == null)
            {
                return NotFound();
            }

            return View(flightReservation);
        }

        // GET: FlightReservations/Create
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult Create()
        {
            ViewData["myFlightId"] = new SelectList(_context.flights, "id", "aircraft");
            ViewData["myUserId"] = new SelectList(_context.users, "idUser", "email");
            return View();
        }

        // POST: FlightReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,myUserId,myFlightId,amountPaid,sites")] FlightReservation flightReservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flightReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["myFlightId"] = new SelectList(_context.flights, "id", "aircraft", flightReservation.myFlightId);
            ViewData["myUserId"] = new SelectList(_context.users, "idUser", "email", flightReservation.myUserId);
            return View(flightReservation);
        }

        // GET: FlightReservations/Edit/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.flightsReservation == null)
            {
                return NotFound();
            }

            var flightReservation = await _context.flightsReservation.FindAsync(id);
            if (flightReservation == null)
            {
                return NotFound();
            }
            ViewData["myFlightId"] = new SelectList(_context.flights, "id", "aircraft", flightReservation.myFlightId);
            ViewData["myUserId"] = new SelectList(_context.users, "idUser", "email", flightReservation.myUserId);
            return View(flightReservation);
        }

        // POST: FlightReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,myUserId,myFlightId,amountPaid,sites")] FlightReservation flightReservation)
        {
            if (id != flightReservation.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightReservationExists(flightReservation.id))
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
            ViewData["myFlightId"] = new SelectList(_context.flights, "id", "aircraft", flightReservation.myFlightId);
            ViewData["myUserId"] = new SelectList(_context.users, "idUser", "email", flightReservation.myUserId);
            return View(flightReservation);
        }

        // GET: FlightReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.flightsReservation == null)
            {
                return NotFound();
            }

            var flightReservation = await _context.flightsReservation
                .Include(f => f.myFlight)
                .Include(f => f.myUser)
                .FirstOrDefaultAsync(m => m.id == id);
            if (flightReservation == null)
            {
                return NotFound();
            }

            return View(flightReservation);
        }

        // POST: FlightReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.flightsReservation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.flightsReservation'  is null.");
            }
            var flightReservation = await _context.flightsReservation.FindAsync(id);
            if (flightReservation != null)
            {
                _context.flightsReservation.Remove(flightReservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightReservationExists(int id)
        {
          return (_context.flightsReservation?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
