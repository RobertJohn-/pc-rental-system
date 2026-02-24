using Microsoft.EntityFrameworkCore;
using PcRental.Domain.Entities;

namespace PcRental.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PcRentalDbContext _context;

        public UserRepository(PcRentalDbContext context)
        {
            _context = context;
        }
        
        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}