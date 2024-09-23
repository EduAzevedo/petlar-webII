using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetLar.Data;
using PetLar.Models;

namespace PetLar.Pages.Home
{
    public class RegisterAnimal : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        public RegisterAnimal(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Animal Animal { get; set; }

        [BindProperty]
        public IFormFile ImageUpload { get; set; }

        public IActionResult OnGet(int? id)
        {
            Console.WriteLine(id);
            if (id == null)
            {
                // Se não houver ID, retorna uma página em branco para cadastro
                Animal = new Animal();
                return Page();
            }

            // Buscar o animal pelo ID
            Animal = _context.Animals.Find(id);
            if (Animal == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;

            if (ModelState.IsValid)
            {
                return Page();
            }

            var idOng = await GetIdOng(email);
            string path = "";

            if (ImageUpload != null && ImageUpload.Length > 0)
            {
                var fileName = Path.GetFileName(ImageUpload.FileName);
                var filePath = Path.Combine(_uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(stream);
                }

                path = $"/images/{fileName}";
            }

            if (Animal.Id > 0) // Se o ID é maior que zero, atualiza o animal existente
            {
                var animalExistente = await _context.Animals.FindAsync(Animal.Id);
                if (animalExistente == null)
                {
                    return NotFound();
                }

                // Caminho completo da imagem
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", animalExistente.ImagePath.TrimStart('/'));

                // Verifica se a imagem existe e a exclui
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                // Atualiza as propriedades
                animalExistente.Name = Animal.Name;
                animalExistente.Species = Animal.Species;
                animalExistente.Breed = Animal.Breed;
                animalExistente.Age = Animal.Age;
                animalExistente.Description = Animal.Description;
                animalExistente.IsAdopted = Animal.IsAdopted;
                animalExistente.ShelterId = Animal.ShelterId;
                if (!string.IsNullOrEmpty(path))
                {
                    animalExistente.ImagePath = path; // Atualiza a imagem se houver uma nova
                }
            }
            else // Se não houver ID, adiciona um novo animal
            {
                var animalNovo = new Animal
                {
                    Name = Animal.Name,
                    Species = Animal.Species,
                    Breed = Animal.Breed,
                    Age = Animal.Age,
                    Description = Animal.Description,
                    IsAdopted = Animal.IsAdopted,
                    ShelterId = Animal.ShelterId,
                    ImagePath = path,
                    OngId = idOng
                };

                _context.Animals.Add(animalNovo);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/Ong/OngProfile");
        }


        public async Task<int> GetIdOng(string email)
        {

            var user = await _context.Users.Include(u => u.Ong).FirstOrDefaultAsync(u => u.Email == email);

            return user.Ong.Id;
        }
    }
}