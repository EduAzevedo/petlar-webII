using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetLar.Models.OngModels
{
    public class Ong
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Phone { get; set; }

        // Relacionamento 1 para 1 com User
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        // Relacionamento 1 para muitos com Animal
        public List<Animal> Animals { get; set; } = new List<Animal>();

    }
}