using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetLar.Data;
using PetLar.Models;
using PetLar.Models.OngModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace PetLar.Pages.OngPages;
public class OngProfileModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public PetLar.Models.OngModels.Ong ong { get; set; }
    public List<Animal> AvailableAnimals { get; set; } = new List<Animal>();

    public OngProfileModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async void OnGet()
    {

        var email = User.FindFirst(ClaimTypes.Name)?.Value;

        var user1 = GetUserInfoByIdAsync(email).Result;
        ong = user1.Ong;
        AvailableAnimals = user1.Ong.Animals ?? new List<Animal>();

        // AvailableAnimals = GetAvailableAnimalsByOng(1);
    }

    public async Task<User> GetUserInfoByIdAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine("Email não ta vazio");
            throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
        }

        Console.WriteLine("User sendo procurado");
        var user = await _context.Users.Include(u => u.Ong)
                                    .ThenInclude(o => o.Animals)
                                    .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            Console.WriteLine("User não tá vazio");
            throw new Exception("User not found.");
        }

        return user;
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        // Busca o animal pelo ID
        var animal = await _context.Animals.FindAsync(id);
        if (animal == null)
        {
            return NotFound();
        }

        // Caminho completo da imagem
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", animal.ImagePath.TrimStart('/'));

        // Verifica se a imagem existe e a exclui
        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }

        // Remove o animal do contexto
        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();

        // Redireciona após a exclusão
        return RedirectToPage("/Ong/OngProfile");
    }

}