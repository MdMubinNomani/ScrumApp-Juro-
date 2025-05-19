using Microsoft.AspNetCore.Mvc;
using ScrumApp__Juro_.Data;

namespace ScrumApp__Juro_.Controllers
{
    public class ModuleController : Controller
    {
        private readonly ScrumDbContext _context;
        public ModuleController(ScrumDbContext context)
        {
            this._context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveStatusUp(int Id)
        {
            var module = _context.Modules.FirstOrDefault(m => m.ModuleID == Id);

            if (module == null)
                return NotFound();

            if (module.Status == "Not Started")
                module.Status = "In Progress";
            else if (module.Status == "In Progress")
                module.Status = "Completed";

            _context.SaveChanges();
            return RedirectToAction("Enter", "Project", new { Id = module.ProjectID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveStatusDown(int Id)
        {
            var module = _context.Modules.FirstOrDefault(m => m.ModuleID == Id);

            if (module == null)
                return NotFound();

            if (module.Status == "In Progress")
                module.Status = "Not Started";
            else if (module.Status == "Completed")
                module.Status = "In Progress";

            _context.SaveChanges();
            return RedirectToAction("Enter", "Project", new { Id = module.ProjectID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int Id)
        {
            var module = _context.Modules.FirstOrDefault(m => m.ModuleID == Id);
            if (module == null)
                return NotFound();
            _context.Modules.Remove(module);
            _context.SaveChanges();
            return RedirectToAction("Enter", "Project", new { Id = module.ProjectID });
        }
    }
}
