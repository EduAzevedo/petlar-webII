using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IActionResult OnGet()
        {
                return Page();
        }

        public async Task<IActionResult> OnPostAsync(){

            if (!ModelState.IsValid){
                return Page();
            }

            if (ImageUpload != null && ImageUpload.Length > 0)
            {
                var fileName = Path.GetFileName(ImageUpload.FileName);
                var filePath = Path.Combine(_uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(stream);
                }

                Animal.ImagePath = $"/images/{fileName}";
            }

            _context.Animals.Add(Animal);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}