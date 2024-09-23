// using Microsoft.EntityFrameworkCore;
// using PetLar.Data;
// using PetLar.Models;

// public class UserRepository : IUserRepository
// {
//     private readonly ApplicationDbContext _context;

//     public UserRepository(ApplicationDbContext context)
//     {
//         _context = context;
//     }

//     public async Task<User> GetUserInfoByIdAsync(int userId)
//     {
//         return await _context.Users
//             .Where(u => u.Id == userId)
//             .Select(u => new User
//             {
//                 Id = u.Id,
//                 UserName = u.UserName,
//                 Email = u.Email,
//             })
//             .FirstOrDefaultAsync();
//     }
// }
