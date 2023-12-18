using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TravelAgency_MVC.Models;
using static TravelAgency_MVC.Controllers.UsersController;

namespace TravelAgency_MVC.Controllers
{
    public class HotelReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HotelReservations
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.hotelReservations.Include(h => h.MyHotel).Include(h => h.MyUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HotelReservations/Details/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.hotelReservations == null)
            {
                return NotFound();
            }

            var hotelReservation = await _context.hotelReservations
                .Include(h => h.MyHotel)
                .Include(h => h.MyUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hotelReservation == null)
            {
                return NotFound();
            }

            return View(hotelReservation);
        }

        // GET: HotelReservations/Create
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult Create()
        {
            ViewData["myHotelId"] = new SelectList(_context.hotel, "Id", "Name");
            ViewData["myUserId"] = new SelectList(_context.users, "idUser", "email");
            return View();
        }

        // POST: HotelReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,myHotelId,myUserId,Since,Until,AmountPaid,quantity")] HotelReservation hotelReservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["myHotelId"] = new SelectList(_context.hotel, "Id", "Name", hotelReservation.myHotelId);
            ViewData["myUserId"] = new SelectList(_context.users, "idUser", "email", hotelReservation.myUserId);
            return View(hotelReservation);
        }

        // GET: HotelReservations/Edit/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.hotelReservations == null)
            {
                return NotFound();
            }

            var hotelReservation = await _context.hotelReservations.FindAsync(id);
            if (hotelReservation == null)
            {
                return NotFound();
            }
            ViewData["myHotelId"] = new SelectList(_context.hotel, "Id", "Name", hotelReservation.myHotelId);
            ViewData["myUserId"] = new SelectList(_context.users, "idUser", "email", hotelReservation.myUserId);
            return View(hotelReservation);
        }

        // POST: HotelReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,myHotelId,myUserId,Since,Until,AmountPaid,quantity")] HotelReservation hotelReservation)
        {
            if (id != hotelReservation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelReservationExists(hotelReservation.ID))
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
            ViewData["myHotelId"] = new SelectList(_context.hotel, "Id", "Name", hotelReservation.myHotelId);
            ViewData["myUserId"] = new SelectList(_context.users, "idUser", "email", hotelReservation.myUserId);
            return View(hotelReservation);
        }

        // GET: HotelReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.hotelReservations == null)
            {
                return NotFound();
            }

            var hotelReservation = await _context.hotelReservations
                .Include(h => h.MyHotel)
                .Include(h => h.MyUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hotelReservation == null)
            {
                return NotFound();
            }

            return View(hotelReservation);
        }

        // POST: HotelReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.hotelReservations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.hotelReservations'  is null.");
            }
            var hotelReservation = await _context.hotelReservations.FindAsync(id);
            if (hotelReservation != null)
            {
                _context.hotelReservations.Remove(hotelReservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelReservationExists(int id)
        {
            return (_context.hotelReservations?.Any(e => e.ID == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> EditHotel()
        {
            return View();
        }


        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> EditHotelUser(int? id)
        {
            if (id == null || _context.hotelReservations == null)
            {
                return NotFound();
            }

            var hotelReservation = await _context.hotelReservations.FindAsync(id);
            if (hotelReservation == null)
            {
                return NotFound();
            }
            ViewData["myHotelId"] = new SelectList(_context.hotel, "Id", "Name", hotelReservation.myHotelId);
            ViewData["myUserId"] = new SelectList(_context.users, "idUser", "email", hotelReservation.myUserId);
            return View(hotelReservation);
        }


        // POST: FlightReservations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHotelUser(int id, [Bind("ID,myHotelId,myUserId,Since,Until,AmountPaid,quantity")] HotelReservation hotelreservation)
        {

            var bdact = _context.hotelReservations.Include(fr => fr.MyUser).Include(fr => fr.MyHotel);

            HotelReservation actual = bdact.FirstOrDefault(e => e.ID == id);



            if (id != hotelreservation.ID)
            {
                TempData["ErrorEnModificacion"] = "No se pudo modificar la reserva";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                int newSites = hotelreservation.quantity - actual.quantity;
                int totalSites = 0;
                if (newSites > 0)
                {
                    totalSites = newSites + hotelreservation.quantity;
                }
                else
                {
                    totalSites = hotelreservation.quantity - newSites;
                }

                int numeroDias = (int)(hotelreservation.Until - hotelreservation.Since).TotalDays;

                double costoCambiar = hotelreservation.quantity * numeroDias * actual.MyHotel.Price - actual.AmountPaid;

                int cantidadTotal = 0;


                var hr = actual.MyHotel.MyReservations.Where(h => h.Since >= hotelreservation.Since && h.Until <= hotelreservation.Until);

                foreach (HotelReservation hrr in hr)
                {
                    cantidadTotal += hrr.quantity;
                }

                if (actual.MyUser.credit >= costoCambiar && actual.MyHotel.Capacity >= cantidadTotal + totalSites && hotelreservation.Since < hotelreservation.Until && hotelreservation.quantity > 0)
                {
                    try
                    {
                        actual.MyUser.credit -= costoCambiar;
                        actual.quantity = hotelreservation.quantity;
                        actual.Since = hotelreservation.Since;
                        actual.Until = hotelreservation.Until;
                        actual.AmountPaid = actual.AmountPaid + costoCambiar;

                        _context.hotelReservations.Update(actual);
                        _context.users.Update(actual.MyUser);
                        _context.hotel.Update(actual.MyHotel);

                        await _context.SaveChangesAsync();
                        TempData["ReservaModificada"] = "Su reserva fue modificada con exito";

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!HotelReservationExists(hotelreservation.ID))
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

    }

}

