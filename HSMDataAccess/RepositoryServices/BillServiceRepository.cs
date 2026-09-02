using HSMDataAccess.Data;
using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSMDataAccess.RepositoryServices
{
    public class BillServiceRepository 
    {
        public AppDBContext _context;

        public BillServiceRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<BillService> AddAsync(BillService entity)
        {
            await _context.Set<BillService>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(BillService entity)
        {
            _context.Set<BillService>().Remove(entity);

            int affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<IEnumerable<BillService>> GetAllAsync()
        {
            return await _context.Set<BillService>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BillService?> GetByIDAsync(
            string serviceID,
            string billID)
        {
            return await _context.Set<BillService>()
                .FirstOrDefaultAsync(e =>
                    e.ServiceID == serviceID &&
                    e.BillID == billID);
        }

        public async Task<bool> UpdateAsync(BillService entity)
        {
            _context.Set<BillService>().Update(entity);

            int affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<bool> ExistsAsync(
            string serviceID,
            string billID)
        {
            return await _context.Set<BillService>()
                .AnyAsync(e =>
                    e.ServiceID == serviceID &&
                    e.BillID == billID);
        }
    }
}