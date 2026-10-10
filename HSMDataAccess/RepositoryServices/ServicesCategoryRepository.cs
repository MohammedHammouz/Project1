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
    public class ServicesCategoryRepository :GenericRepository<ServiceCategories>
    {
  
        public ServicesCategoryRepository(AppDBContext context) : base(context)
        {
        }
        public async Task<bool> ExistsAsync(string serviceID)
        {
            return await _context.Set<ServiceCategories>()
                .AnyAsync(e =>e.ID == serviceID);
        }
    }
}
