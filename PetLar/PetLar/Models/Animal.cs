using System;
using System.ComponentModel.DataAnnotations;

namespace PetLar.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Species { get; set; }

        public string Breed { get; set; }

        public int Age { get; set; }

        public string Description { get; set; }

        public bool IsAdopted { get; set; }

        public int ShelterId { get; set; }
    }
}
