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
    public class PatientRepository : GenericRepository<Patient>
    {
        public PatientRepository(AppDBContext context) : base(context)
        {
        }
        public async Task<int> CountNumberOfPersonIsOne(string PersonID)
        {
            var count = _context.Patients.CountAsync(p => p.PersonID == PersonID);
            return await count;
        }
    }
}
