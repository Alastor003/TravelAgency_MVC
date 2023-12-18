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
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HotelsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Hotels
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Index(string searchCity)
        {
            IQueryable<Hotel> hotelsQuery = _context.hotel.Include(h => h.Location);

            if (!string.IsNullOrEmpty(searchCity))
            {
                hotelsQuery = hotelsQuery.Where(h => h.Location.cityName.Contains(searchCity));
            }

            var hotels = await hotelsQuery.ToListAsync();

            return View(hotels);
        }

        // GET: Hotels/Details/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.hotel == null)
            {
                return NotFound();
            }

            var hotel = await _context.hotel
                .Include(h => h.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // GET: Hotels/Create
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult Create()
        {
            ViewData["locationId"] = new SelectList(_context.cities, "id", "id");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,locationId,Capacity,Price,Name")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["locationId"] = new SelectList(_context.cities, "id", "id", hotel.locationId);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.hotel == null)
            {
                return NotFound();
            }

            var hotel = await _context.hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["locationId"] = new SelectList(_context.cities, "id", "id", hotel.locationId);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,locationId,Capacity,Price,Name")] Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.Id))
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
            ViewData["locationId"] = new SelectList(_context.cities, "id", "id", hotel.locationId);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.hotel == null)
            {
                return NotFound();
            }

            var hotel = await _context.hotel
                .Include(h => h.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.hotel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.hotel'  is null.");
            }
            var hotel = await _context.hotel.FindAsync(id);
            if (hotel != null)
            {
                List<HotelReservation> hotelReservations = _context.hotelReservations
                    .Where(reserva => reserva.MyHotel.Id == id)
                    .ToList();

                if (hotelReservations.Count != 0)
                {
                    foreach (HotelReservation hr in hotelReservations)
                    {
                        if (hr.Until > DateTime.Now)
                        {
                            hr.MyUser.DepositCredit(hr.AmountPaid);
                            hr.MyUser.myHotelBookings.Remove(hr);
                            _context.users.Update(hr.MyUser);
                        }
                    }
                    _context.hotelReservations.RemoveRange(hotelReservations);
                }

                _context.hotel.Remove(hotel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Reserve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(string idUser, int idHotel, int people, DateTime dSince, DateTime dUntil)
        {
            int id = int.Parse(idUser);
            User user = await _context.users.FindAsync(id);
            Hotel hotel = await _context.hotel.FindAsync(idHotel);

            if (user == null || hotel == null)
            {
                TempData["ErrorMessage"] = "No se cumplen los requisitos";
                return RedirectToAction("Index");
            }

            int numberDays = (int)(dUntil - dSince).TotalDays;
            double totalCost = hotel.Price * people * numberDays;

            var hr = hotel.MyReservations.Where(h => h.Since >= dSince && h.Until <= dUntil);
            int cantidadTotal = 0;
            foreach (HotelReservation hrr in hr)
            {
                cantidadTotal += hrr.quantity;
            }



            if (user.credit >= totalCost && hotel.Capacity >= cantidadTotal + people && dSince < dUntil)
            {
                try
                {
                    user.credit -= totalCost;

                    HotelReservation hotelRes = new HotelReservation(hotel, user, dSince, dUntil, totalCost, people);

                    var existe = _context.usersHotels.FirstOrDefault(u => u.idHotel == idHotel && u.idUser == id);

                    if (existe != null)
                    {
                        existe.cantidad++;
                        _context.usersHotels.Update(existe);
                    }
                    else
                    {
                        user.historyHotelBookings.Add(hotel);
                        hotel.Hosts.Add(user);

                    }


                    _context.hotelReservations.Add(hotelRes);
                    user.myHotelBookings.Add(hotelRes);
                    _context.users.Update(user);
                    _context.hotel.Update(hotel);

                    PlaySuccessSound(_hostingEnvironment);


                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Reserva realizada con exito";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return NotFound(e);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "No se cumplen los requisitos";
                return RedirectToAction("Index");
            }
        }

        public List<Hotel> FindHotelsByCity(string searchName)
        {
            var matchingHotels = _context.hotel
                .Where(h => h.Location.cityName.Contains(searchName))
                .ToList();

            return matchingHotels;
        }

        private bool HotelExists(int id)
        {
            return (_context.hotel?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void PlaySuccessSound(IWebHostEnvironment hostingEnvironment)
        {
            string soundFilePath = "/songs/confirm.wav";

            var webRoot = hostingEnvironment.WebRootPath;
            var fileInfo = hostingEnvironment.WebRootFileProvider.GetFileInfo(soundFilePath);
            var physicalPath = fileInfo.PhysicalPath;

            if (System.IO.File.Exists(physicalPath))
            {
                using (var soundPlayer = new System.Media.SoundPlayer(physicalPath))
                {
                    soundPlayer.Play();
                }
            }
        }
    }
}
