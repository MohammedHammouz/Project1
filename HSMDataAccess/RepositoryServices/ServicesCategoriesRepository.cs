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
    public class ServicesCategoriesRepository : GenericRepository<ServicesCategoriesEntity>
    {
        public ServicesCategoriesRepository(AppDBContext context) : base(context)
        {

        }
        public async Task<ServicesCategoriesEntity> GetByID(int id)
        {
            var entity = await _context.Set<EmployeeEntity>()
            .FirstOrDefaultAsync(e => e.EmployeeID == id);
            return entity;
        }
    }
}
