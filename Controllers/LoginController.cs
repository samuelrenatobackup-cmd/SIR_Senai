using Microsoft.AspNetCore.Mvc;
using SIR.Contexts;
using SIR.Models;

namespace SIR.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            Usuario? usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Senha == senha);

            if (usuario == null)
            {
                ViewBag.Erro = "Email ou senha inválidos";
                return View("Index");
            }

            return RedirectToAction("Index", "Reserva");
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            {
                ViewBag.ErroCadastro = "Este email já está cadastrado.";
                return View("Index");
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            ViewBag.Sucesso = "Cadastro realizado com sucesso!";
            return View("Index");
        }
    }
}