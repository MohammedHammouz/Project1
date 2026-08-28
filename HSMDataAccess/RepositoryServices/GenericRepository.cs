using HSMDataAccess.Data;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HSMDataAccess.RepositoryServices
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public AppDBContext _context;

        public GenericRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            
            int AffectedRows= await _context.SaveChangesAsync();
            return AffectedRows > 0;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _context.Set<T>().AsNoTracking().ToListAsync();
            return entities;
        }
        
        public async Task<T> GetByIDAsync(string ID)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => EF.Property<string>(e, "ID") == ID);
            return entity;
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            int AffectedRows = await _context.SaveChangesAsync();
            Console.WriteLine($"AffectedRows: {AffectedRows}");
            return AffectedRows > 0;
        }
        public async Task<bool> ExistsAsync(string ID)
        {
            return await _context.Set<T>()
                .AnyAsync(e => EF.Property<string>(e, "ID") == ID);
        }
    }
}
