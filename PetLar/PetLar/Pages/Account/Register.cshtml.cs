using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetLar.Data;
using PetLar.Models;
using PetLar.Models.OngModels;
using System.ComponentModel.DataAnnotations;

namespace PetLar.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        private readonly string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        public readonly ApplicationDbContext _context;

        [BindProperty]
        public User user { get; set; } = new User();

        [BindProperty]
        public PetLar.Models.OngModels.Ong ong { get; set; }
        [BindProperty]
        public IFormFile ImageUpload { get; set; }

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                // Se não houver ID, retorna uma página em branco para cadastro
                PetLar.Models.OngModels.Ong ong = new PetLar.Models.OngModels.Ong();
                return Page();
            }

            // Buscar o animal pelo ID
            ong = _context.Ongs
                                .Include(o => o.User)
                                .FirstOrDefaultAsync(o => o.Id == id).Result;
            user = ong.User ?? new Models.User();
            Console.WriteLine(ong.Id);
            if (ong == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string path = "";

            // Lógica para fazer upload da imagem
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

            // Se estiver editando (ou seja, se o ID da ONG existir)
            if (ong.Id > 0)
            {
                var ongExistente = await _context.Ongs.Include(o => o.User).FirstOrDefaultAsync(o => o.Id == ong.Id);
                if (ongExistente != null)
                {
                    // Atualizar dados da ONG existente
                    ongExistente.Name = ong.Name;
                    ongExistente.Address = ong.Address;
                    ongExistente.Description = ong.Description;
                    ongExistente.Phone = ong.Phone;
                    if (!string.IsNullOrEmpty(path))
                    {
                        ongExistente.ImagePath = path; // Atualiza a imagem se uma nova for enviada
                    }

                    // Atualizar dados do usuário associado
                    if (ongExistente.User != null)
                    {
                        ongExistente.User.Email = user.Email;
                        ongExistente.User.Password = user.Password; // Considere fazer hash da senha antes de salvar
                    }

                    _context.Ongs.Update(ongExistente);
                }
            }
            else
            {
                // Criar um novo usuário
                var usuarioNovo = new User
                {
                    Email = user.Email,
                    Password = user.Password,
                    Role = "ONG"
                };

                _context.Users.Add(usuarioNovo);
                await _context.SaveChangesAsync();

                // Criar uma nova ONG
                var novaOng = new PetLar.Models.OngModels.Ong
                {
                    Name = ong.Name,
                    Address = ong.Address,
                    Description = ong.Description,
                    UserId = usuarioNovo.Id,
                    ImagePath = path,
                    Phone = ong.Phone
                };

                _context.Ongs.Add(novaOng);
            }

            // Salvar as alterações no contexto
            await _context.SaveChangesAsync();

            // Redirecionar para a página de registro, sem ID
            return RedirectToPage("/Account/Register");
        }


    }

    public class RegisterViewModel
    {

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas n�o coincidem.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}