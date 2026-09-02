using HSMDataAccess.Data;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.RepositoryServices
{
    public class ReportRepository : GenericRepository<Report>
    {
        public ReportRepository(AppDBContext context) : base(context)
        {

        }
    }
}
