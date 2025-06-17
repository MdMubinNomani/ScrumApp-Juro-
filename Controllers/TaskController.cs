using Microsoft.AspNetCore.Mvc;
using ScrumApp__Juro_.Data;
using ScrumApp__Juro_.Models.Entities;

namespace ScrumApp__Juro_.Controllers
{
    public class TaskController : Controller
    {
        private readonly ScrumDbContext _context;
        public TaskController(ScrumDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int id)
        {
            var submodule = _context.SubModules.FirstOrDefault(m => m.SubModuleID == id);

            if (submodule == null)
                return NotFound();

            var task = new ScrumApp__Juro_.Models.Entities.Task
            {
                SubModuleID = submodule.SubModuleID,
                ProjectID = submodule.ProjectID,
                CreatedAt = DateTime.Now,
                Status = "Not Started"
            };

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScrumApp__Juro_.Models.Entities.Task task)
        {
            if (ModelState.IsValid)
            {
                task.Status = "Not Started";
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Enter", "Project", new { id = task.ProjectID });
            }

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveStatusUp(int Id)
        {
            var task = _context.Tasks.FirstOrDefault(m => m.TaskID == Id);

            if (task == null)
                return NotFound();

            if (task.Status == "Not Started")
                task.Status = "In Progress";
            else if (task.Status == "In Progress")
                task.Status = "Completed";

            _context.SaveChanges();
            return RedirectToAction("Enter", "Project", new { Id = task.ProjectID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveStatusDown(int Id)
        {
            var task = _context.Tasks.FirstOrDefault(m => m.TaskID == Id);

            if (task == null)
                return NotFound();

            if (task.Status == "In Progress")
                task.Status = "Not Started";
            else if (task.Status == "Completed")
                task.Status = "In Progress";

            _context.SaveChanges();
            return RedirectToAction("Enter", "Project", new { Id = task.ProjectID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int Id)
        {
            var task = _context.Tasks.FirstOrDefault(m => m.TaskID == Id);
            if (task == null)
                return NotFound();
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("Enter", "Project", new { Id = task.ProjectID });
        }
    }
}
