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
            var applicationDbContext = _context.flights.Include(f => f.destination).Include(f => f.origin).Include(f => f.passengers);
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
            var cityOri = _context.cities.FirstOrDefault(cOri => cOri.id == flight.originId);
            var cityDest = _context.cities.FirstOrDefault(cDest => cDest.id == flight.destinationId);

            if (ModelState.IsValid)
            {
                cityOri.flights.Add(flight);
                cityDest.flights.Add(flight);
                _context.cities.Update(cityOri);
                _context.cities.Update(cityDest);
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

            int solded = _context.flightsReservation
                  .Where(reserva => reserva.myFlight != null && reserva.myFlight.id == id)
                  .Sum(reserva => reserva.myFlight.soldFlights);

            if (flight.capacity < solded)
            {
                ModelState.AddModelError("capacity", "La capacidad restante no puede ser menor a los boletos vendidos");
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
                if (flight.allFlights.Count != 0 && flight.date > DateTime.Now)
                {
                    foreach (FlightReservation fr in flight.allFlights)
                    {
                        fr.myUser.DepositCredit(fr.amountPaid);
                        fr.myUser.myFlightBookings.Remove(fr);
                        _context.users.Update(fr.myUser);
                    }
                }
                _context.flights.Remove(flight);
                _context.flightsReservation.RemoveRange(flight.allFlights);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
          return (_context.flights?.Any(e => e.id == id)).GetValueOrDefault();
        }

        [HttpPost, ActionName("Reserve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(string idUser, int idFlight, int sites)
        {
            int id = int.Parse(idUser);
            var flight = await _context.flights.FindAsync(idFlight);
            User user = await _context.users.FindAsync(id);
            FlightReservation fr = _context.flightsReservation.FirstOrDefault(f => f.myFlightId == idFlight && f.myUserId == id);

            if (flight == null && user == null)
            {
                TempData["ErrorMessage"] = "No se cumplen los requisitos";
                return RedirectToAction("Index");
            }


            double totalCost = flight.flightPrice * sites;

            if (user.credit >= totalCost && flight.soldFlights + sites <= flight.capacity && fr == null)
            {

                user.credit -= totalCost;

                FlightReservation flightRes = new FlightReservation(flight, user, totalCost, sites);

                user.myFlightBookings.Add(flightRes);
                user.historyFlightBookings.Add(flight);
                flight.passengers.Add(user);
                flight.allFlights.Add(flightRes);
                flight.soldFlights += sites;

                _context.flights.Update(flight);
                _context.users.Update(user);
                _context.flightsReservation.Add(flightRes);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Reserva realizada con exito";
                return RedirectToAction(nameof(Index));

            }

            TempData["ErrorMessage"] = "No se cumplen los requisitos";
            return RedirectToAction("Index");

        }

        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Search(string searchCity, DateTime? startDate, DateTime? endDate)
        {
            if (!string.IsNullOrEmpty(searchCity) && startDate.HasValue && endDate.HasValue)
            {
                var searchResults = SearchFlights(searchCity, startDate.Value, endDate.Value);
                return View("Index", searchResults);
            }

            return RedirectToAction("Index");
        }

        public List<Flight> SearchFlights(string searchCity, DateTime startDate, DateTime endDate)
        {
            var availableFlights = _context.flights
                .Where(f =>
                    f.destination.cityName.Contains(searchCity) &&
                    f.date.Date >= startDate.Date &&
                    f.date.Date <= endDate.Date &&
                    f.capacity > f.passengers.Count)
                .ToList();

            return availableFlights;
        }

    }
}
