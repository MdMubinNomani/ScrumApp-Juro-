using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumApp__Juro_.Data;
using ScrumApp__Juro_.Models.Entities;
using ScrumApp__Juro_.ViewModels;

namespace ScrumApp__Juro_.Controllers
{
    public class DeveloperController : Controller
    {

        private readonly ScrumDbContext _context;
        public DeveloperController(ScrumDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var Vdev = await _context.Developers.FindAsync(id);
            var Vmodules = await _context.Modules
                .Where(m => m.DeveloperID == id)
                .ToListAsync();
            var Vsubmodules = await _context.SubModules
                .Where(s => s.DeveloperID == id)
                .ToListAsync();
            var Vtasks = await _context.Tasks
                .Where(t => t.DeveloperID == id)
                .ToListAsync();

            var viewModel = new DeveloperModuleViewModel
            {
                developer = Vdev,
                modules = Vmodules,
                submodules = Vsubmodules,
                tasks = Vtasks
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignDeveloperModule(int ModuleID, int DeveloperID)
        {
            var module = await _context.Modules.FindAsync(ModuleID);
            var developer = await _context.Developers.FindAsync(DeveloperID);

            if (module == null || developer == null)
            {
                return NotFound();
            }

            module.DeveloperID = developer.DeveloperID;
            _context.Modules.Update(module);
            await _context.SaveChangesAsync();
            return RedirectToAction("Enter", "Project", new { id = module.ProjectID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignDeveloperSubModule(int SubModuleID, int DeveloperID)
        {
            var submodule = await _context.SubModules.FindAsync(SubModuleID);
            var developer = await _context.Developers.FindAsync(DeveloperID);

            if (submodule == null || developer == null)
            {
                return NotFound();
            }

            submodule.DeveloperID = developer.DeveloperID;
            _context.SubModules.Update(submodule);
            await _context.SaveChangesAsync();
            return RedirectToAction("Enter", "Project", new { id = submodule.ProjectID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignDeveloperTask(int TaskID, int DeveloperID)
        {
            var task = await _context.Tasks.FindAsync(TaskID);
            var developer = await _context.Developers.FindAsync(DeveloperID);

            if (task == null || developer == null)
            {
                return NotFound();
            }

            task.DeveloperID = developer.DeveloperID;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Enter", "Project", new { id = task.ProjectID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateModuleStatus(int ModuleID, string status)
        {
            var module = await _context.Modules.FindAsync(ModuleID);
            if (module == null)
            {
                return NotFound();
            }
            module.Status = status;
            _context.Modules.Update(module);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Developer", new {id = module.DeveloperID});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSubModuleStatus(int SubModuleID, string status)
        {
            var submodule = await _context.SubModules.FindAsync(SubModuleID);
            if (submodule == null)
            {
                return NotFound();
            }
            submodule.Status = status;
            _context.SubModules.Update(submodule);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Developer", new { id = submodule.DeveloperID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTaskStatus(int TaskID, string status)
        {
            var task = await _context.Tasks.FindAsync(TaskID);
            if (task == null)
            {
                return NotFound();
            }
            task.Status = status;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Developer", new { id = task.DeveloperID });
        }
    }
}
