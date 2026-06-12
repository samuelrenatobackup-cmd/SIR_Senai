using Microsoft.AspNetCore.Mvc;
using SIR.Contexts;
using SIR.Models;
using System.Linq;

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

        // ================= LOGIN =================
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
            {
                ViewBag.Erro = "Email ou senha inválidos";
                return View("Index");
            }

            return RedirectToAction("Index", "Reserva");
        }

        // ================= CADASTRO =================
        [HttpPost]
        public IActionResult Cadastro(string nome, string sobrenome, string email, string senha, string confirmarSenha)
        {
            // valida senha igual
            if (senha != confirmarSenha)
            {
                ViewBag.ErroCadastro = "As senhas não coincidem.";
                return View("Index");
            }

            // email já existe
            if (_context.Usuarios.Any(u => u.Email == email))
            {
                ViewBag.ErroCadastro = "Este email já está cadastrado.";
                return View("Index");
            }

            // senha forte
            if (!SenhaForte(senha))
            {
                ViewBag.ErroCadastro =
                    "Senha fraca! Use no mínimo 8 caracteres, com maiúscula, minúscula, número e símbolo.";
                return View("Index");
            }

            var usuario = new Usuario
            {
                Nome = nome,
                Sobrenome = sobrenome,
                Email = email,
                Senha = BCrypt.Net.BCrypt.HashPassword(senha),
                TipoUsuario = false
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            TempData["Sucesso"] = "Cadastro realizado com sucesso!";
            return RedirectToAction("Index");
        }

        // ================= VALIDA SENHA =================
        private bool SenhaForte(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                return false;

            if (senha.Length < 8)
                return false;

            bool maiuscula = senha.Any(char.IsUpper);
            bool minuscula = senha.Any(char.IsLower);
            bool numero = senha.Any(char.IsDigit);
            bool especial = senha.Any(c => !char.IsLetterOrDigit(c));

            return maiuscula && minuscula && numero && especial;
        }
    }
}