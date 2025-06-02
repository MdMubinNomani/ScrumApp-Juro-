using Microsoft.AspNetCore.Mvc;
using ScrumApp__Juro_.Data;
using ScrumApp__Juro_.Models.Entities;

namespace ScrumApp__Juro_.Controllers
{
    public class DeveloperController : Controller
    {

        private readonly ScrumDbContext _context;
        public DeveloperController(ScrumDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (TempData["DeveloperID"] == null)
            {
                return RedirectToAction("DevLogin", "Auth");
            }

            int devId = (int)TempData["DeveloperID"];
            var dev = await _context.Developers.FindAsync(devId);
            return View(dev);
        }
    }
}
