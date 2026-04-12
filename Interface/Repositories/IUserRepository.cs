using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamMSAppMVC.Models;
using ExamMSAppMVC.Interface.Repositories;

using ExamMSAppMVC.Models.Entities;

namespace ExamMSAppMVC.Interface.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByPasswordAsync(string password);
        Task<User> GetUserByRegistrationNumberAsync(string registrationNumber);
        Task<User> GetByEmailAsync(string email);
        // Task<User> DeleteAsync(User user);

    }
}