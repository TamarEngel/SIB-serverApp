using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.Repositories;

namespace web.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.UserList.Include(u => u.UserCreationList).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.UserList.Include(u => u.UserCreationList).FirstOrDefaultAsync(user => user.UserId == id && !user.IsDeleted);
        }
        public async Task<User> GetUserByIdentityAsync(int identity)
        {
            return await _context.UserList.Include(u => u.UserCreationList).FirstOrDefaultAsync(user => user.Id == identity && !user.IsDeleted);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.UserList.Include(u => u.UserCreationList).FirstOrDefaultAsync(user => user.Email == email && !user.IsDeleted);
        }

        public async Task<bool> AddAdminAsync(User user)
        {
            if (await _context.UserList.AnyAsync(c => c.UserId == user.UserId))
                return false;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.PasswordHash = passwordHash;
            user.Role = ERole.Admin;
            user.IsDeleted = false;

            await _context.UserList.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            if (await _context.UserList.AnyAsync(c => c.UserId == user.UserId))
                return false;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.PasswordHash = passwordHash;
            user.Role = ERole.User;
            user.IsDeleted = false;

            await _context.UserList.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsync(int id, User user)
        {
            var currentUser = await GetUserByIdAsync(id);
            if (currentUser != null)
            {
                currentUser.Name = user.Name;
                currentUser.Email = user.Email;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if(user == null)
                return false;
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
