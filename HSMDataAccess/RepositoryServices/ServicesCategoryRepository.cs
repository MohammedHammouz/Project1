using HSMDataAccess.Data;
using HSMDataAccess.Entities;

namespace HSMDataAccess.RepositoryServices
{
    public class ServicesCategoryRepository : GenericRepository<ServiceCategory>
    {
        public ServicesCategoryRepository(AppDBContext context) : base(context)
        {
        }
    }
}
