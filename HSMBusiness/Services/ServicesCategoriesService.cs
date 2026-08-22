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
    public class ServicesCategoriesService
    {
    //    public int CategoryID { get; set; }
    //    public string CategoryName { get; set; } = null!;
    //    public string? CategoryDescription { get; set; }
    //    public ServicesCategoriesDTO servicesCategoriesDTO { get { return new ServicesCategoriesDTO(CategoryID, CategoryName, CategoryDescription); } }
    //    public enum enMode { Add,Update}
    //    public enMode Mode = enMode.Add;
    //    private readonly ServicesCategoriesRepository _repository;
    //    public ServicesCategories(ServicesCategoriesDTO servicesCategoriesDTO, ServicesCategoriesRepository repository, enMode  mode=enMode.Add)
    //    {
    //        this.CategoryID = servicesCategoriesDTO.CategoryID;
    //        this.CategoryName = servicesCategoriesDTO.CategoryName;
    //        this.CategoryDescription = servicesCategoriesDTO.CategoryDescription;
    //        _repository = repository;
    //        Mode = mode;
    //    }
    //    public async Task<List<ServicesCategoriesDTO>> GetAll()
    //    {
    //        var patients = await _repository.GetAllAsync();
    //        return patients.Select(p => new ServicesCategoriesDTO(p.CategoryID,p.CategoryName,p.CategoryDescription))
    //            .ToList();
    //    }
    //    public async Task<ServicesCategoriesDTO> GetByID(int CategoryID)
    //    {
    //        var category = await _repository.GetByID(CategoryID);
    //        if (category == null)
    //        {
    //            return new ServicesCategoriesDTO();
    //        }
    //        return new ServicesCategoriesDTO(category.CategoryID,category.CategoryName,category.CategoryDescription);
    //    }
    //    private async Task<bool> _AddNew()
    //    {
    //        ServicesCategoriesEntity? servicesCategoriesEntity = new ServicesCategoriesEntity();
    //        servicesCategoriesEntity.CategoryID = servicesCategoriesDTO.CategoryID;
    //        servicesCategoriesEntity.CategoryName = servicesCategoriesDTO.CategoryName;
    //        servicesCategoriesEntity.CategoryDescription = servicesCategoriesDTO.CategoryDescription;
    //        var NewCategory = await _repository.AddAsync(servicesCategoriesEntity);
    //        this.CategoryID = NewCategory.CategoryID;
    //        return this.CategoryID != -1;
    //    }
    //    private async Task<bool> _Update()
    //    {
    //        ServicesCategoriesEntity? servicesCategoriesEntity = new ServicesCategoriesEntity();
    //        servicesCategoriesEntity.CategoryName = servicesCategoriesDTO.CategoryName;
    //        servicesCategoriesEntity.CategoryDescription = servicesCategoriesDTO.CategoryDescription;
    //        return await _repository.UpdateAsync(servicesCategoriesEntity);
    //    }
    //    public async Task<bool> Save()
    //    {
    //        switch (Mode)
    //        {
    //            case enMode.Add:
    //                if (await _AddNew())
    //                {
    //                    Mode = enMode.Update;
    //                    return true;
    //                }
    //                else
    //                {
    //                    return false;
    //                }
    //            case enMode.Update:
    //                return await _Update();
    //        }
    //        return false;
    //    }
    //    public async Task<bool> Delete(int CategoryID)
    //    {
    //        ServicesCategoriesDTO patient = await GetByID(CategoryID);
    //        if (patient == null)
    //        {
    //            return false;
    //        }
    //        ServicesCategoriesEntity? servicesCategoriesEntity = new ServicesCategoriesEntity();
    //        servicesCategoriesEntity.CategoryID = CategoryID;
    //        servicesCategoriesEntity.CategoryID = servicesCategoriesDTO.CategoryID;
    //        servicesCategoriesEntity.CategoryName = servicesCategoriesDTO.CategoryName;
    //        servicesCategoriesEntity.CategoryDescription = servicesCategoriesDTO.CategoryDescription;
    //        return await _repository.DeleteAsync(servicesCategoriesEntity);
    //    }
    }
}
