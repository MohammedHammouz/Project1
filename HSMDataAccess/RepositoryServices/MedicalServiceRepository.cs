using HSMDataAccess.Data;
using HSMDataAccess.Entities;

namespace HSMDataAccess.RepositoryServices
{
    public class MedicalServiceRepository : GenericRepository<MedicalService>
    {
        public MedicalServiceRepository(AppDBContext context) : base(context)
        {
        }
    }
}
