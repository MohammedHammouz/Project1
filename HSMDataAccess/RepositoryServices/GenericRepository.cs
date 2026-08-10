using HSMDataAccess.Data;
using HSMDataAccess.RepositoryServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HSMDataAccess.RepositoryServices
{
    public class GenericRepository<T>
    {
        public AppDBContext _context;

    }
}
