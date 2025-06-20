using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ScrumApp__Juro_.Data;
using ScrumApp__Juro_.Models.Entities;
using ScrumApp__Juro_.ViewModels;

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

        public IActionResult Index(int ManagerID)
        {
            var projects = _context.Projects
                .Include(p => p.Modules)
                .Where(p => p.ManagerID == ManagerID) 
                .ToList();

            return View(projects);
        }


        // GET: Project/Create
        public IActionResult Create()
        {
            ViewBag.ManagerID = TempData["ManagerID"];
            return View();
        }


        // Post: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, Description, CreatedAt, ManagerID")] Project project)
        {
            if (project.ManagerID == 0)
            {
                if (TempData["ManagerID"] != null)
                {
                    project.ManagerID = Convert.ToInt32(TempData["ManagerID"]);
                }
                else
                {
                    return BadRequest("ManagerID not found.");
                }
            }

            project.Modules = new List<Module>();
            _context.Add(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { ManagerID = project.ManagerID });
        }


        // GET: Project/Enter/?id=
        public IActionResult Enter(int Id)
        {
            var PVM = FetchAllByProjectID(Id);
            return View(PVM);
        }

        public ProjectsViewModel FetchAllByProjectID(int Id)
        {
            var PVM = new ProjectsViewModel();
            PVM.Project = _context.Projects.FirstOrDefault(p => p.ProjectID == Id);
            PVM.Modules = _context.Modules.Include(m => m.SubModules)
                .ThenInclude(s => s.Tasks)
                .Where(m => m.ProjectID == Id)
                .ToList();
            PVM.SubModules = _context.SubModules.Include(s => s.Tasks)
                .Where(m => m.ProjectID == Id)
                .ToList();
            PVM.Tasks = _context.Tasks.Where(m => m.ProjectID == Id)
                .ToList();

            PVM.Developers = _context.Developers.ToList();

            return PVM;
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

            return RedirectToAction("Index", new { ManagerID = project.ManagerID });
        }


    }
}
