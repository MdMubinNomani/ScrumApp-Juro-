using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumApp__Juro_.Data;
using ScrumApp__Juro_.Models.Entities;

namespace ScrumApp__Juro_.Controllers
{
    public class SubmoduleController : Controller
    {
        private readonly ScrumDbContext _context;
        public SubmoduleController(ScrumDbContext context)
        {
            this._context = context;
        }

        // GET: Submodule/Index
        public IActionResult Index(int Id)
        {
            var submodules = _context.SubModules
                .Where(s => s.ModuleID == Id) 
                .ToList();
            return View(submodules);
        }

        // GET: Submodule/Create

        public IActionResult Create(int id)
        {
            var submodule = new SubModule
            {
                ModuleID = id
            };
            return View(submodule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubModule submodule)
        {
            submodule.Status = "Not Started";
            await _context.AddAsync(submodule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { Id = submodule.ModuleID });
        }

    }
}
