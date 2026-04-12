using ExamMSAppMVC.Contracts.Entities;
using ExamMSAppMVC.EMSDBcontext;
using global::ExamMSAppMVC.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



namespace ExamMSAppMVC.Implementation.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly EMSDbContext _context;

        public BaseRepository(EMSDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(Guid id) => await _context.Set<T>()
        .FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>()
        .ToListAsync();

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

