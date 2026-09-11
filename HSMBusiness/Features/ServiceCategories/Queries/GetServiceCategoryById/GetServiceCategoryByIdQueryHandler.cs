using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.ServiceCategories.Dtos;
using HSMBusiness.Features.ServiceCategories.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.ServiceCategories.Queries.GetServiceCategoryById
{
    public class GetServiceCategoryByIdQueryHandler(
        IValidator<GetServiceCategoryByIdQuery> validator,
        ServicesCategoryRepository serviceCategoryRepository)
    {
        public async Task<Result<ServiceCategoryDto>> ExecuteAsync(GetServiceCategoryByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var serviceCategory = await serviceCategoryRepository.GetByIDAsync(query.Id);
            if (serviceCategory is null)
            {
                return ServiceCategoryErrors.NotFound;
            }

            return serviceCategory.ToDto();
        }
    }
}
