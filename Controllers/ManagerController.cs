using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using ScrumApp__Juro_.Data;

namespace ScrumApp__Juro_.Controllers
{
    public class ManagerController : Controller
    {

        private readonly ScrumDbContext _context;
        public ManagerController(ScrumDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
