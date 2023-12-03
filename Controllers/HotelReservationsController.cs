using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency_MVC.Models;

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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.hotelReservations.Include(h => h.MyHotel).Include(h => h.MyUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HotelReservations/Details/5
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
    }
}
