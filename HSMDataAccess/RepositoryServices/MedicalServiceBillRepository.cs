using HSMDataAccess.Data;
using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSMDataAccess.RepositoryServices
{
    public class MedicalServiceBillRepository 
    {
        public AppDBContext _context;

        public MedicalServiceBillRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<MedicalServiceBill> AddAsync(MedicalServiceBill entity)
        {
            await _context.Set<MedicalServiceBill>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(MedicalServiceBill entity)
        {
            _context.Set<MedicalServiceBill>().Remove(entity);

            int affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<IEnumerable<MedicalServiceBill>> GetAllAsync()
        {
            return await _context.Set<MedicalServiceBill>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MedicalServiceBill?> GetByIDAsync(
            string serviceID,
            string billID)
        {
            return await _context.Set<MedicalServiceBill>()
                .FirstOrDefaultAsync(e =>
                    e.ServiceID == serviceID &&
                    e.BillID == billID);
        }

        public async Task<bool> UpdateAsync(MedicalServiceBill entity)
        {
            _context.Set<MedicalServiceBill>().Update(entity);

            int affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<bool> ExistsAsync(
            string serviceID,
            string billID)
        {
            return await _context.Set<MedicalServiceBill>()
                .AnyAsync(e =>
                    e.ServiceID == serviceID &&
                    e.BillID == billID);
        }
    }
}