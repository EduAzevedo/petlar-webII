using System;
using System.ComponentModel.DataAnnotations;
using PetLar.Models.OngModels;

namespace PetLar.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool IsAdopted { get; set; }

        public int ShelterId { get; set; }
        public string? ImagePath { get; set; }


        // Relacionamento com Ong
        public int OngId { get; set; }
        public Ong Ong { get; set; }
    }
}
