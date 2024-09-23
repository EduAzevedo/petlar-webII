using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetLar.Data;
using PetLar.Models;

namespace PetLar.Pages.Home
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Animal> Animais { get; set; }
    
        public async Task OnGetAsync()
        {
            Animais = await _context.Animals
                .Include(a => a.Ong) // Assumindo que a relação entre Animal e Ong é chamada de Ong
                .ToListAsync();
        }
    }
}
