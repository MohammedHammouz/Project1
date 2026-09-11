using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.ServiceCategories.Dtos;
using HSMBusiness.Features.ServiceCategories.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.ServiceCategories.Commands.UpdateServiceCategory
{
    public class UpdateServiceCategoryCommandHandler(
        IValidator<UpdateServiceCategoryCommand> validator,
        ServicesCategoryRepository serviceCategoryRepository)
    {
        public async Task<Result<ServiceCategoryDto>> ExecuteAsync(UpdateServiceCategoryCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var serviceCategory = await serviceCategoryRepository.GetByIDAsync(command.Id);
            if (serviceCategory is null)
            {
                return ServiceCategoryErrors.NotFound;
            }

            serviceCategory.CategoryName = command.CategoryName;
            serviceCategory.CategoryDescription = command.CategoryDescription;

            await serviceCategoryRepository.UpdateAsync(serviceCategory);

            return serviceCategory.ToDto();
        }
    }
}
