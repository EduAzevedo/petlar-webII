using Azure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetLar.Models;
using System.Collections.Generic;

namespace PetLar.Pages.Ong;

    public class OngProfileModel : PageModel
    {
        public string OngName { get; set; }
        public List<Animal> AvailableAnimals { get; set; }

        public void OnGet(int id)
        {

            var user = GetOngById(id);
            OngName = user.UserName;

            AvailableAnimals = GetAvailableAnimalsByOng(id);
        }

        private User GetOngById(int id)
        {
            return new User { Id = id, UserName = "Amor de Patas" };
        }

        private List<Animal> GetAvailableAnimalsByOng(int ongId)
        {
            return new List<Animal>
            {
                new Animal { Name = "Rex", ImagePath = "rex.jpg" },
                new Animal { Name = "Luna", ImagePath = "luna.jpg" },
                new Animal { Name = "Luna", ImagePath = "luna.jpg" },

                new Animal { Name = "Luna", ImagePath = "luna.jpg" },
                new Animal { Name = "Luna", ImagePath = "luna.jpg" },
                new Animal { Name = "Bob", ImagePath = "bob.jpg" }
            };
        }
    }