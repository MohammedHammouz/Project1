using HSMDataAccess.Data;
using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.RepositoryServices
{
    public class ServicesCategoryRepository 
    {
        private AppDBContext _context;
        public ServicesCategoryRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistsAsync(
           string serviceID)
        {
            return await _context.Set<ServiceCategory>()
                .AnyAsync(e =>
                    e.ID == serviceID);
        }
    }
}
