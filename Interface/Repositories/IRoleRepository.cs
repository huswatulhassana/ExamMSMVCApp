using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Models.Entities;

namespace ExamMSAppMVC.Interface.Service
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> GetByNameAsync(string name);

    }
}