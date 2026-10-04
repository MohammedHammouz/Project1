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
    public class PersonRepository:GenericRepository<Person>
    {
        public PersonRepository(AppDBContext context) : base(context)
        {
        }
        
        public async Task<int> CountNumberOfContactNumberIsOne(string ContactNumber)
        {
            var count = _context.People.CountAsync(p => p.ContactNumber == ContactNumber);
            return await count;
        }
        public async Task<int> CountNumberOfEmailIsOne(string Email)
        {
            var count = _context.People.CountAsync(p => p.Email == Email);
            return await count;
        }
    }
}
