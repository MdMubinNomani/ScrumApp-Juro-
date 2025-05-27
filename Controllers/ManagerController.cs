using Microsoft.AspNetCore.Mvc;

namespace ScrumApp__Juro_.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
