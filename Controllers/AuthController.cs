using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumApp__Juro_.Data;
using ScrumApp__Juro_.Models.Entities;

namespace ScrumApp__Juro_.Controllers
{
    public class AuthController : Controller
    {

        private readonly ScrumDbContext _context;
        public AuthController(ScrumDbContext context)
        {
            this._context = context;
        }

        public IActionResult ManLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManLogin(UserAuth auth)
        {
            var user = await _context.UserAuths
            .FirstOrDefaultAsync(u => u.Username == auth.Username && u.Password == auth.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(auth);
            }
            else
            {
                return RedirectToAction("Index", "Manager");
            }
        }

        public IActionResult ManRegister()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManRegister(UserAuth user)
        {
            var exists = await _context.UserAuths.AnyAsync(u => u.Username == user.Username && u.Password == user.Password);
            if (exists)
            {
                ModelState.AddModelError("", "Username already exists.");
                return View(user);
            }

            _context.UserAuths.Add(user);
            var manager = new Manager
            {
                Name = user.Name,
                Email = user.Email,
                Username = user.Username
            };
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();

            TempData["SignupSuccess"] = "Account created successfully!";
            return RedirectToAction("ManLogin");
        }

        public IActionResult DevLogin()
        {
            return View();
        }

    }
}
