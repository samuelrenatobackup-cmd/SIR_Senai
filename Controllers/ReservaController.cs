using Microsoft.AspNetCore.Mvc;
namespace SIR.Controllers
{
    public class ReservaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}