using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authentication;

namespace PetLar.Pages.OngPages
{
    public class Logout : PageModel
    {
        private readonly ILogger<Logout> _logger;

        public Logout(ILogger<Logout> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
    {
        // Executa o logout do usuário
        await HttpContext.SignOutAsync("CookieAuth");
        
        // Redireciona para a página inicial ou login após o logout
        return RedirectToPage("/Index");
    }
    }
}