using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetLar.Data;
using PetLar.Models;
using System.ComponentModel.DataAnnotations;

namespace PetLar.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        public readonly ApplicationDbContext _context;

        [BindProperty]
        public User user { get; set; } = new User();

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // M�todo chamado quando a p�gina � carregada com uma requisi��o GET
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var usuarioNovo = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password
            };

            _context.Users.Add(usuarioNovo);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

    }

    public class RegisterViewModel
    {

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas n�o coincidem.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}