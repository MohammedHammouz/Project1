using HSMBusiness.Mappers;
using HSMDataAccess.DTOs;
using HSMDataAccess.RepositoryServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Services
{
    public class GenericService<TEntity, TDTO> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repository;
        public enum enMode { Add = 0, Update }
        public enMode Mode = enMode.Add;
        public GenericService(IGenericRepository<TEntity> repository,enMode Mode = enMode.Add)
        {
            _repository = repository;
            this.Mode = Mode;
        }
        
        public async Task<TDTO?> GetByID(string id)
        {
            var entity = await _repository.GetByIDAsync(id);

            if (entity == null)
                return default;

            return MapToDTO(entity);
        }

        public async Task<List<TDTO>> GetAll()
        {
            var entities = await _repository.GetAllAsync();

            return entities
                .Select(MapToDTO)
                .ToList();
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await _repository.GetByIDAsync(id);

            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        protected virtual TDTO MapToDTO(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
