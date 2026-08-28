using HSMDataAccess.Data;
using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.RepositoryServices
{
    public class UserRepository : GenericRepository<User>
    {
        

        public UserRepository(AppDBContext context) : base(context)
        {

        }

    }
}
