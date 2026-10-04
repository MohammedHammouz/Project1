using HSMDataAccess.Data;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.RepositoryServices
{
    public class DoctorRepository : GenericRepository<Doctor>
    {
        public DoctorRepository(AppDBContext context) : base(context)
        {
            
        }
        public async Task<int> CountNumberOfUserIDIsOne(string UserID)
        {
            var count = _context.Doctors.CountAsync(d => d.UserID == UserID);
            return await count;
        }
    }
}
