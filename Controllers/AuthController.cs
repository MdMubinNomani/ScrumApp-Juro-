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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DevLogin(UserAuth auth)
        {
            var user = await _context.Developers
                .FirstOrDefaultAsync(u => u.Username == auth.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(auth);
            }

            var pass = await _context.UserAuths
                .SingleAsync(u => u.Username == user.Username);

            if (pass.Password != auth.Password)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(auth);
            }
            else
            {
                return RedirectToAction("Index", "Developer", new {id = user.DeveloperID });
                //return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult DevRegister()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DevRegister(RegisterViewModel data)
        {
            var dev = data.Developer;
            var exists = await _context.Developers.FirstOrDefaultAsync(d => d.Username == data.UserAuth.Username);

            if (exists != null)
            {
                ModelState.AddModelError("", "Username already exists.");
                return View(data);
            }

            _context.UserAuths.Add(data.UserAuth);
            var developer = new Developer
            {
                Name = data.Developer.Name,
                Email = data.Developer.Email,
                Username = data.UserAuth.Username
            };
            _context.Developers.Add(developer);
            await _context.SaveChangesAsync();

            TempData["SignupSuccess"] = "Account created successfully!";
            return RedirectToAction("DevLogin");
        }

    }
}
