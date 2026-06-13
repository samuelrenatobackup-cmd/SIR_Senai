using Microsoft.AspNetCore.Mvc;
namespace SIR.Controllers

{
    public class AdminController : Controller
    { 

        
        
        public IActionResult Index()
        {
            return View();
        }
    }
}