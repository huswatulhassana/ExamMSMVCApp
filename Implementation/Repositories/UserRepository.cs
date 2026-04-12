using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamMSAppMVC.Implementation.Repositories;
using ExamMSAppMVC.Models;
using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.EMSDBcontext;
using Microsoft.EntityFrameworkCore;

namespace ExamMSAppMVC.Implementation.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly EMSDbContext _EMSDbContext;

        public UserRepository(EMSDbContext emsDbContext) : base(emsDbContext)
        {
            _EMSDbContext = emsDbContext;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _EMSDbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetUserByRegistrationNumberAsync(string registrationNumber)
        {
            return await _EMSDbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.RegistrationNumber == registrationNumber);
        }

        public async Task<User?> GetUserByPasswordAsync(string password)
        {
            return await _EMSDbContext.Users
                .FirstOrDefaultAsync(u => u.HashPassword == password);
        }

        public async Task<User> DeleteAsync(User user)
        {
            _EMSDbContext.Users.Remove(user);
            await _EMSDbContext.SaveChangesAsync();
            return user;
        }
    }
}

