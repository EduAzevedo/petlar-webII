using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetLar.Data;
using PetLar.Models;
using System.Security.Claims;

namespace PetLar.Pages.Account
{
    public class Login : PageModel
    {
        private readonly ApplicationDbContext _context;
        // private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<Login> _logger;

        public Login(ILogger<Login> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
            // _passwordHasher = passwordHasher;
        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine($"Username: {Username}, Password: {Password}");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Username);
            if (user != null)
            {
                Console.WriteLine(user.Email);
                // Verificar se a senha fornecida corresponde ao hash armazenado
                // var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, Password);

                if (Password == user.Password)
                {
                    Console.WriteLine($"Usuario Logado: {user.Email}, Senha: {user.Password}");
                    // Credenciais corretas, criar as claims e autenticar o usuário
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true // Manter a sessão ativa
                    };

                    await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

                    // No seu método de login, após autenticar
                    return RedirectToPage("/Ong/OngProfile");

                }
            }

            // Se falhar, retorna à página de login
            return Page();
        }
    }
}