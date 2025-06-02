using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumApp__Juro_.Data;
using ScrumApp__Juro_.Models.Entities;
using ScrumApp__Juro_.ViewModels;

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
                var manager = await _context.Managers
                    .FirstOrDefaultAsync(m => m.Username == user.Username);

                TempData["ManagerID"] = manager.ManagerID; 
                return RedirectToAction("Index", "Project", new { ManagerID = manager.ManagerID});
            }
        }


        public IActionResult ManRegister()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManRegister(RegisterViewModel data)
        {
            var exists = await _context.UserAuths.AnyAsync(u => u.Username == data.UserAuth.Username && u.Password == data.UserAuth.Password);
            if (exists)
            {
                ModelState.AddModelError("", "Username already exists.");
                return View(data);
            }

            _context.UserAuths.Add(data.UserAuth);
            var manager = new Manager
            {
                Name = data.Manager.Name,
                Email = data.Manager.Email,
                Username = data.UserAuth.Username
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
