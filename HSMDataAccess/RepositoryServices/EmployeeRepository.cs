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
    public class EmployeeRepository : GenericRepository<Employee>
    {
        public EmployeeRepository(AppDBContext context) : base(context)
        {
                        
        }
        public async Task<Employee> GetByID(int ID)
        {
            var entity = await _context.Set<Employee>().FirstOrDefaultAsync(e => EF.Property<int>(e, "ID") == ID);
            return entity;
        }
    }
}
