using HSMDataAccess.Data;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.RepositoryServices
{
    public class DepartmentRepository : GenericRepository<Department>
    {
        public DepartmentRepository(AppDBContext context) : base(context)
        {

        }
    }
}
