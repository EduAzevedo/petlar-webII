using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PetLar.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        public void OnGet()
        {
            // M�todo chamado quando a p�gina � carregada com uma requisi��o GET
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Adicione a l�gica para o registro do usu�rio aqui
            // Por exemplo: salvar os dados em um banco de dados, enviar um e-mail, etc.

            return RedirectToPage("/Index"); // Redireciona para a p�gina inicial ap�s o registro
        }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas n�o coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
