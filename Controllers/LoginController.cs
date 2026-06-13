using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SIR.Contexts;
using SIR.Models;

namespace SIR.Controllers
{
    public class LoginController : Controller
    {
        private readonly ContextoBancoDados _context;

        public LoginController(ContextoBancoDados context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AutenticarUsuario(string email, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null || 
                !BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
            {
                ViewBag.Erro = "Email ou senha inválidos";
                return View("Index");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario ? "Admin" : "Usuario")
            };

            var identidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identidade);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                });

            if (usuario.TipoUsuario)
                return RedirectToAction("Index", "Admin");

            return RedirectToAction("ListarEquipamentos", "Reserva");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CadastrarUsuario(
            string nome,
            string sobrenome,
            string email,
            string senha,
            string confirmarSenha)
        {
            if (senha != confirmarSenha)
            {
                ViewBag.ErroCadastro = "As senhas não coincidem.";
                return View("Index");
            }

            if (_context.Usuarios.Any(u => u.Email == email))
            {
                ViewBag.ErroCadastro = "Este email já está cadastrado.";
                return View("Index");
            }

            if (!ValidarForcaSenha(senha))
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

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }

        private bool ValidarForcaSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha)) return false;
            if (senha.Length < 8) return false;

            return senha.Any(char.IsUpper) &&
                   senha.Any(char.IsLower) &&
                   senha.Any(char.IsDigit) &&
                   senha.Any(c => !char.IsLetterOrDigit(c));
        }
    }
}