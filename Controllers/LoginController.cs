using Microsoft.AspNetCore.Mvc;

namespace SIR.Controllers;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}