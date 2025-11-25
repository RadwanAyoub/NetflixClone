using Microsoft.EntityFrameworkCore;
using NetflixClone.Application.Interfaces.Repositories;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserProfiles)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted && u.IsActive);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.UserProfiles)
                .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted && u.IsActive);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email && !u.IsDeleted && u.IsActive);
        }
    }
}