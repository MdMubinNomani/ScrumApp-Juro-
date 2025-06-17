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
            var module = _context.Modules.FirstOrDefault(m => m.ModuleID == id);

            if (module == null)
                return NotFound();

            var subModule = new SubModule
            {
                ModuleID = module.ModuleID,
                ProjectID = module.ProjectID,
                CreatedAt = DateTime.Now,
                Status = "Not Started" 
            };

            return View(subModule);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubModule subModule)
        {
            if (ModelState.IsValid)
            {
                subModule.Status = "Not Started";
                _context.SubModules.Add(subModule);
                await _context.SaveChangesAsync();
                return RedirectToAction("Enter", "Project", new { id = subModule.ProjectID });
            }

            return View(subModule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveStatusUp(int Id)
        {
            var submodule = _context.SubModules.FirstOrDefault(m => m.SubModuleID == Id);

            if (submodule == null)
                return NotFound();

            if (submodule.Status == "Not Started")
                submodule.Status = "In Progress";
            else if (submodule.Status == "In Progress")
                submodule.Status = "Completed";

            _context.SaveChanges();
            return RedirectToAction("Enter", "Project", new { Id = submodule.ProjectID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveStatusDown(int Id)
        {
            var submodule = _context.SubModules.FirstOrDefault(m => m.SubModuleID == Id);

            if (submodule == null)
                return NotFound();

            if (submodule.Status == "In Progress")
                submodule.Status = "Not Started";
            else if (submodule.Status == "Completed")
                submodule.Status = "In Progress";

            _context.SaveChanges();
            return RedirectToAction("Enter", "Project", new { Id = submodule.ProjectID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int Id)
        {
            var submodule = _context.SubModules.FirstOrDefault(m => m.SubModuleID == Id);
            if (submodule == null)
                return NotFound();
            _context.SubModules.Remove(submodule);
            _context.SaveChanges();
            return RedirectToAction("Enter", "Project", new { Id = submodule.ProjectID });
        }

    }
}
