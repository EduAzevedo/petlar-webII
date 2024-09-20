using Microsoft.AspNetCore.Mvc.RazorPages;
using PetLar.Models;
using System.Collections.Generic;

namespace PetLar.Pages.Animals
{
    public class AnimalsModel : PageModel
    {
        public List<Animal> Animais { get; set; }

        public void OnGet()
        {
            Animais = GetAllAnimals();
        }

        private List<Animal> GetAllAnimals()
        {
            return new List<Animal>
    {
        new Animal
        {
            Name = "Rex",
            Species = "C�o",
            Breed = "Labrador",
            Age = 1,
            Description = "Um labrador amig�vel e brincalh�o.",
            ImagePath = "/images/animal-1.jpeg"
        },
        new Animal
        {
            Name = "Luna",
            Species = "Gato",
            Breed = "Persa",
            Age = 5,
            Description = "Uma gata elegante e tranquila.",
            ImagePath = "/images/animal-3.jpeg"
        }
    };
        }
    }
}
