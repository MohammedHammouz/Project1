using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Services
{
    public class ServicesCategoryService
    {
        ResultPatern resultPattern = new ResultPatern();
        public enum enMode { Add = 0, Update }
        public enMode _mode = enMode.Add;
        private readonly ServicesCategoryRepository _servicesCategoryRepository;
        public ServicesCategoryService(ServicesCategoryRepository servicesCategoryRepository,enMode mode=enMode.Add)
        {
            _servicesCategoryRepository = servicesCategoryRepository;
            _mode = mode;
        }
        public async Task<(int Status,string? Response,bool IsSuccess, List<ServicesCategoryDTO>? ListOfServiceCategories)> GetAll()
        {
            var ServiceCategories = await _servicesCategoryRepository.GetAllAsync();
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (ServiceCategories == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, null);
            }
            return (response.Status, response.Response, response.IsSuccess,ServiceCategories.Select(SC=>new ServiceCategoryMapper().ToDTO(SC)).ToList());
        }
        public async Task<(int Status, string? Response, bool IsSuccess,ServicesCategoryDTO? servicesCategoryDTO)>GetByID(string ID)
        {
            var ServiceCategory = await _servicesCategoryRepository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (ServiceCategory == null)
            {
                
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, null);
            }
            return (response.Status, response.Response, response.IsSuccess, new ServiceCategoryMapper().ToDTO(ServiceCategory));
        }
        private async Task<bool>IsExist(string ID)
        {
            return await _servicesCategoryRepository.ExistsAsync(ID);
        }
        private async Task<(int Status,bool IsSuccess)> _Add(ServicesCategoryDTO servicesCategoryDTO)
        {
            var ServiceCategory =  new ServiceCategoryMapper().ToEntity(servicesCategoryDTO);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            var AddNew = await _servicesCategoryRepository.AddAsync(ServiceCategory);
            return (response.Status, AddNew.ID != "");
        }
        private async Task<(int, string?, bool)>_Update(string ID,ServicesCategoryDTO servicesCategoryDTO)
        {
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (!await IsExist(ID))
            {
                return (response.Status, response.Response, response.IsSuccess);
            }
            var CurrServiceCategory = await _servicesCategoryRepository.GetByIDAsync(ID);
            CurrServiceCategory = new ServiceCategoryMapper().ToEntity(servicesCategoryDTO);
            return ((response.Status, response.Response, await _servicesCategoryRepository.UpdateAsync(CurrServiceCategory)));
            
        }
        public async Task<(int Status, string? Response, bool IsSuccess)> Save(ServicesCategoryDTO servicesCategoryDTO, string? ID="")
        {
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            switch (_mode)
            {
                case enMode.Add:
                    var AddNew = await _Add(servicesCategoryDTO);
                    if (AddNew.IsSuccess)
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response = await resultPattern.GiveResponse(AddNew.Status, new Dictionary<string, string>());
                        
                    }
                    return (response.Status, response.Response, response.IsSuccess);
                case enMode.Update:
                    return await _Update(ID, servicesCategoryDTO);
            }
            return (response.Status, response.Response, response.IsSuccess);
        }
        public async Task<(int Status, string? Response, bool IsSuccess)> Delete(string ID)
        {
            var ServiceCategory = await _servicesCategoryRepository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (ServiceCategory == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            bool IsDeleted = await _servicesCategoryRepository.DeleteAsync(ServiceCategory);
            if (!IsDeleted)
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            return (response.Status, response.Response, response.IsSuccess);
        }
    }
}
