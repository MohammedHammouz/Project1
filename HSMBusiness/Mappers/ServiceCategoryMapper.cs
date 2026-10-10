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

        public ServiceCategories ToEntity(ServicesCategoryDTO serviceCategoryDTO,ServiceCategories serviceCategory=null,enMode mode = enMode.Add)
        {
            if (mode == enMode.Add)
            {
                return new ServiceCategories
                {
                    CategoryName = serviceCategoryDTO.CategoryName,
                    CategoryDescription = serviceCategoryDTO.CategoryDescription
                };
            }
            else
            {
                serviceCategory.CategoryName = serviceCategoryDTO.CategoryName;
                serviceCategory.CategoryDescription = serviceCategoryDTO.CategoryDescription;
            }
            return serviceCategory;
        }

        public ServicesCategoryDTO ToDTO(ServiceCategories serviceCategory)
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
