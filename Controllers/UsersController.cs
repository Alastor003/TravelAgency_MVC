﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency_MVC.Models;

namespace TravelAgency_MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.users != null ? 
                          View(await _context.users.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var user = await _context.users
                .FirstOrDefaultAsync(m => m.idUser == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idUser,dni,name,surname,email,password,failedTries,lockedUser,credit,isAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idUser,dni,name,surname,email,password,failedTries,lockedUser,credit,isAdmin")] User user)
        {
            if (id != user.idUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.idUser))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var user = await _context.users
                .FirstOrDefaultAsync(m => m.idUser == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.users'  is null.");
            }
            var user = await _context.users.FindAsync(id);
            if (user != null)
            {
                _context.users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.users?.Any(e => e.idUser == id)).GetValueOrDefault();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string txtEmail, string txtPassword)
        {
            // Verificar las credenciales del usuario
            var user = _context.users.SingleOrDefault(u => u.email == txtEmail && u.password == txtPassword);

            if (user != null)
            {
                HttpContext.Session.SetString("UsuarioAutenticado", user.name);
                HttpContext.Session.SetString("isAdmin", user.isAdmin.ToString());
                HttpContext.Session.SetString("Id", user.idUser.ToString());



                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Credenciales incorrectas";
                return View();
            }
        }

        [HttpPost]
        public ActionResult ProcesarRegistro(string txtNombre, string txtApellido, int txtDNI, string txtEmail, string txtPassword)
        {
            try
            {
                // Verificar si el usuario ya existe en la base de datos
                var existingUser = _context.users.SingleOrDefault(u => u.email == txtEmail);

                if (existingUser != null)
                {
                    ViewBag.Error = "El usuario ya existe.";
                    return View();
                }

                var newUser = new User
                {
                    name = txtNombre,
                    surname = txtApellido,
                    dni = txtDNI,
                    email = txtEmail,
                    password = txtPassword,
                };

                _context.users.Add(newUser);
                _context.SaveChanges();

                // Redirigir a la página de inicio de sesión sin establecer la sesión del usuario autenticado
                return RedirectToAction("Login", "Users");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error durante el registro.";
                return View();
            }
        }



        public ActionResult Volver()
        {
            return RedirectToAction("Login", "Users");
        }



        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Users");
        }
        public async Task<IActionResult> Profile()
        {

            // Obtener el nombre del usuario autenticado desde la sesión
            var authenticatedUserName = HttpContext.Session.GetString("Id");

            // Obtener el usuario actual desde la base de datos
            var currentUser = await _context.users.SingleOrDefaultAsync(u => u.idUser.ToString() == authenticatedUserName);

            if (currentUser == null)
            {
                // Manejar el caso en que no se encuentra el usuario
                return NotFound();
            }

            return View(currentUser);
        }
    }
}
