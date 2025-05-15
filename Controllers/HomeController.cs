using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ScrumApp__Juro_.Data;
using ScrumApp__Juro_.Models;

namespace ScrumApp__Juro_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ScrumDbContext _context;

        public HomeController(ILogger<HomeController> logger, ScrumDbContext context)
        {
            _logger = logger;
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
