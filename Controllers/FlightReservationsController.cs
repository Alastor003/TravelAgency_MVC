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
        public async Task<IActionResult> EditFlights(int? id)
        {
            if (id == null || _context.flightsReservation == null)
            {
                return NotFound();
            }

            var fl = await _context.flightsReservation
                .FirstOrDefaultAsync(fl => fl.id == id);

            if (fl == null)
            {
                return NotFound();
            }
            return View(fl);
        }

        [HttpPost, ActionName("EditFlights")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFlights(int id, [Bind("id,myUserId,myFlightId,amountPaid,sites")] FlightReservation fl)
        {
            var numberOfSeats = _context.flightsReservation
                            .Where(fr => fr.id == id)
                            .Select(fr => fr.sites)
                            .FirstOrDefault(); 

            var flight = _context.flights.FirstOrDefault(f => f.id == fl.myFlightId);
            var user = _context.users.FirstOrDefault(u => u.idUser == fl.myUserId);

            if (id != fl.id)
            {
                return NotFound();
            }

            var newSites = numberOfSeats - fl.sites;

            if (ModelState.IsValid)
            {
                try
                {
                    if (flight.capacity > flight.soldFlights + newSites)
                    {
                        if (user.credit < flight.flightPrice * newSites)
                        {
                            TempData["CreditAlert"] = "No tienes suficiente crédito para comprar estos asientos.";
                            return RedirectToAction("EditFlights", new { id = fl.id });
                        } else
                        {
                            if (newSites > 0)
                            {
                                user.credit -= flight.flightPrice * newSites;
                                fl.amountPaid += flight.flightPrice * newSites;
                                //DepositCredit(fl.myUserId, -flight.flightPrice * newSites); 
                            }
                            else
                            {
                                user.credit += flight.flightPrice * newSites;
                                fl.amountPaid -= flight.flightPrice * newSites;
                                //DepositCredit(fl.myUserId, (flight.flightPrice * newSites) * -1);
                            }
                        }

                        flight.soldFlights += newSites;

                        _context.flightsReservation.Update(fl);
                        _context.flights.Update(flight);
                        _context.users.Update(user);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightReservationExists(fl.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Profile", "Users");
            }
            return View(fl);
        }

        public async Task<IActionResult> DeleteFlight(int? id)
        {
            if (id == null || _context.flightsReservation == null)
            {
                return null;
            }

            var flightReservation = await _context.flightsReservation
                 .Include(f => f.myFlight)
                 .Include(f => f.myUser)
                 .SingleOrDefaultAsync(m => m.id == id);

            if (flightReservation == null)
            {
                return NotFound();
            }

            return View(flightReservation);
        }

        [HttpPost, ActionName("DeleteFlight")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteFlight(int id)
        {
            if (_context.flightsReservation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.flightsReservation'  is null.");
            }
            var flightReservation = await _context.flightsReservation.FindAsync(id);
            var flight = _context.flights.FirstOrDefault(f => f.id == flightReservation.myFlightId);
            if (flightReservation != null)
            {
                if(flight.date > DateTime.Now)
                {
                    DepositCredit(flightReservation.myUserId, flightReservation.amountPaid);
                    flightReservation.myUser.myFlightBookings.Remove(flightReservation);
                    flight.passengers.Remove(flightReservation.myUser);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Profile", "Users");
        }

        public void DepositCredit(int idUser, double amount)
        {
            User user = _context.users.FirstOrDefault(u => u.idUser == idUser);

            if (user != null && amount > 0)
            {
                user.credit += amount;
                _context.users.Update(user);
                _context.SaveChangesAsync();
            }
        }
    }
}
