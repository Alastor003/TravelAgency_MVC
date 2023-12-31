﻿using System;
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

        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> EditFlightUser(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFlightUser(int id, [Bind("id,myUserId,myFlightId,amountPaid,sites")] FlightReservation flightReservation)
        {

            var bdact = _context.flightsReservation.Include(fr => fr.myUser).Include(fr => fr.myFlight);

            FlightReservation actual = bdact.FirstOrDefault(f => f.id == id);



            if (id != flightReservation.id)
            {
                TempData["ErrorEnModificacion"] = "No se pudo modificar la reserva";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                int newSites = flightReservation.sites - actual.sites;
                double costoCambiar = newSites * actual.myFlight.flightPrice;

                if (actual.myUser.credit >= costoCambiar && actual.myFlight.capacity >= actual.myFlight.soldFlights + newSites && flightReservation.sites > 0)
                {
                    try
                    {
                        actual.myUser.credit -= costoCambiar;
                        actual.sites = flightReservation.sites;
                        actual.amountPaid = actual.myFlight.flightPrice * flightReservation.sites;
                        actual.myFlight.soldFlights += newSites;

                        _context.flightsReservation.Update(actual);
                        _context.users.Update(actual.myUser);
                        _context.flights.Update(actual.myFlight);

                        await _context.SaveChangesAsync();
                        TempData["ReservaModificada"] = "Su reserva fue modificada con exito";

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

                }
                else
                {
                    TempData["ErrorEnModificacion"] = "No se pudo modificar la reserva";
                    return RedirectToAction("Profile", "Users");
                }
                return RedirectToAction("Profile", "Users");

            }
            TempData["ErrorEnModificacion"] = "No se pudo modificar la reserva";
            return RedirectToAction("Profile", "Users");

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
            var flightReservation = await _context.flightsReservation
                .Include(f => f.myFlight).Include(f => f.myFlight.passengers)
                .Include(f => f.myUser)
                .FirstOrDefaultAsync(m => m.id == id);

            if (flightReservation != null)
            {
                if (flightReservation.myFlight.date > DateTime.Now)
                {
                    flightReservation.myUser.credit += flightReservation.amountPaid; 
                    flightReservation.myUser.myFlightBookings.Remove(flightReservation);
                    flightReservation.myFlight.passengers.Remove(flightReservation.myUser);
                    flightReservation.myFlight.soldFlights -= flightReservation.sites;
                    
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Profile", "Users");
        }


    }
}
