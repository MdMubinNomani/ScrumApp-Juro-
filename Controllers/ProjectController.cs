using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using ScrumApp__Juro_.Data;
using ScrumApp__Juro_.Models.Entities;

namespace ScrumApp__Juro_.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ScrumDbContext _context;
        public ProjectController(ScrumDbContext context)
        {
            this._context = context;
        }

        // GET: Project
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            return View();
        }

        // Post: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, Description, CreatedAt")] Project project)
        {
            project.Modules = new List<Module>();
            _context.Add(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Project/Enter/?id=
        public IActionResult Enter(int Id)
        {
            var project = _context.Projects
                .Include(p => p.Modules)
                .FirstOrDefault(p => p.ProjectID == Id);

            if (project == null) return NotFound();

            return View(project);
        }

        public IActionResult CreateModule(int Id)
        {
            Module module = new Module();
            module.ProjectID = Id;
            module.Status = "Not Started";
            return View(module);
        }

        // Post: Project/CreateModule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModule(Module module)
        {
            module.Status = "Not Started";

            var project = await _context.Projects
                .Include(p => p.Modules)
                .FirstOrDefaultAsync(p => p.ProjectID == module.ProjectID);

            if (project == null)
                return NotFound();

            project.Modules ??= new List<Module>();

            project.Modules.Add(module);
            _context.Modules.Add(module); 
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Enter), new { Id = module.ProjectID });
        }

        // Post : Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
