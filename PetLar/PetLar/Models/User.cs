using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using PetLar.Models.OngModels;

namespace PetLar.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }

        // Relacionamento 1 para 1 com Ong
        public Ong Ong { get; set; }
    }
}
