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
    public class UsersHotelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersHotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UsersHotels
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.usersHotels.Include(u => u.hotel).Include(u => u.user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UsersHotels/Details/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.usersHotels == null)
            {
                return NotFound();
            }

            var usersHotels = await _context.usersHotels
                .Include(u => u.hotel)
                .Include(u => u.user)
                .FirstOrDefaultAsync(m => m.idUser == id);
            if (usersHotels == null)
            {
                return NotFound();
            }

            return View(usersHotels);
        }

        // GET: UsersHotels/Create
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult Create()
        {
            ViewData["idHotel"] = new SelectList(_context.hotel, "Id", "Name");
            ViewData["idUser"] = new SelectList(_context.users, "idUser", "email");
            return View();
        }

        // POST: UsersHotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idUser,idHotel,cantidad")] UsersHotels usersHotels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersHotels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idHotel"] = new SelectList(_context.hotel, "Id", "Name", usersHotels.idHotel);
            ViewData["idUser"] = new SelectList(_context.users, "idUser", "email", usersHotels.idUser);
            return View(usersHotels);
        }

        // GET: UsersHotels/Edit/5
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.usersHotels == null)
            {
                return NotFound();
            }

            var usersHotels = await _context.usersHotels.FindAsync(id);
            if (usersHotels == null)
            {
                return NotFound();
            }
            ViewData["idHotel"] = new SelectList(_context.hotel, "Id", "Name", usersHotels.idHotel);
            ViewData["idUser"] = new SelectList(_context.users, "idUser", "email", usersHotels.idUser);
            return View(usersHotels);
        }

        // POST: UsersHotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idUser,idHotel,cantidad")] UsersHotels usersHotels)
        {
            if (id != usersHotels.idUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersHotels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersHotelsExists(usersHotels.idUser))
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
            ViewData["idHotel"] = new SelectList(_context.hotel, "Id", "Name", usersHotels.idHotel);
            ViewData["idUser"] = new SelectList(_context.users, "idUser", "email", usersHotels.idUser);
            return View(usersHotels);
        }

        // GET: UsersHotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.usersHotels == null)
            {
                return NotFound();
            }

            var usersHotels = await _context.usersHotels
                .Include(u => u.hotel)
                .Include(u => u.user)
                .FirstOrDefaultAsync(m => m.idUser == id);
            if (usersHotels == null)
            {
                return NotFound();
            }

            return View(usersHotels);
        }

        // POST: UsersHotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usersHotels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.usersHotels'  is null.");
            }
            var usersHotels = await _context.usersHotels.FindAsync(id);
            if (usersHotels != null)
            {
                _context.usersHotels.Remove(usersHotels);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersHotelsExists(int id)
        {
          return (_context.usersHotels?.Any(e => e.idUser == id)).GetValueOrDefault();
        }
    }
}
