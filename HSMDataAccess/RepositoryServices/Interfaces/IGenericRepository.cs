using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.RepositoryServices.Interfaces
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIDAsync(string ID);
        Task<T> AddAsync(T Entity);
        Task<bool> UpdateAsync(T Entity);
        Task<bool> DeleteAsync(T Entity);
   
    }
}
