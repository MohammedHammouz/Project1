using HSMBusiness.Features.ServiceCategories.Dtos;
using HSMBusiness.Features.ServiceCategories.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.ServiceCategories.Queries.GetServiceCategories
{
    public class GetServiceCategoriesQueryHandler(ServicesCategoryRepository serviceCategoryRepository)
    {
        public async Task<Result<List<ServiceCategoryDto>>> ExecuteAsync(GetServiceCategoriesQuery query)
        {
            var serviceCategories = await serviceCategoryRepository.GetAllAsync();

            return serviceCategories.ToDtos();
        }
    }
}
