using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.EMSDBcontext;
using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Interface.Service;
using Microsoft.EntityFrameworkCore;

namespace ExamMSAppMVC.Implementation.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly EMSDbContext _EMSDbContext;

        public RoleRepository(EMSDbContext emsDbContext) : base(emsDbContext)
        {
            _EMSDbContext = emsDbContext;
        }
        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _EMSDbContext.Roles
                .FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());
        }
    }
}