using Microsoft.AspNetCore.Mvc;

namespace MyProFile.Server.Controllers
{
    public class CreditsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
