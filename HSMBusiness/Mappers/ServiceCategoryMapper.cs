using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class ServiceCategoryMapper
    {
        public enum enMode { Add, Update }

        public ServiceCategory ToEntity(
            ServicesCategoryDTO serviceCategoryDTO,
            enMode mode = enMode.Add)
        {
            if (mode == enMode.Add)
            {
                return new ServiceCategory
                {
                    ID = serviceCategoryDTO.ID,
                    CategoryName = serviceCategoryDTO.CategoryName,
                    CategoryDescription = serviceCategoryDTO.CategoryDescription
                };
            }
            else
            {
                return new ServiceCategory
                {
                    ID = serviceCategoryDTO.ID,
                    CategoryName = serviceCategoryDTO.CategoryName,
                    CategoryDescription = serviceCategoryDTO.CategoryDescription
                };
            }
        }

        public ServicesCategoryDTO ToDTO(ServiceCategory serviceCategory)
        {
            return new ServicesCategoryDTO
            (
                serviceCategory.ID,
                serviceCategory.CategoryName,
                serviceCategory.CategoryDescription
            );
        }
    }
}
