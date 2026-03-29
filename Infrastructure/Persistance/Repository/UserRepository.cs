

using Application.Interface;
using Domain.Aggregate.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
           await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
           return await _context.Users.FirstOrDefaultAsync(u =>  u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
           return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);   
        }
    }
}
