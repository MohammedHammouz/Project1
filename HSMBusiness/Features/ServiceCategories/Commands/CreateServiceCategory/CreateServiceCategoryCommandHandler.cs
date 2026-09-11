using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Features.ServiceCategories.Dtos;
using HSMBusiness.Features.ServiceCategories.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.ServiceCategories.Commands.CreateServiceCategory
{
    public class CreateServiceCategoryCommandHandler(
        IValidator<CreateServiceCategoryCommand> validator,
        ServicesCategoryRepository serviceCategoryRepository)
    {
        public async Task<Result<ServiceCategoryDto>> ExecuteAsync(CreateServiceCategoryCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var serviceCategory = new ServiceCategory
            {
                CategoryName = command.CategoryName,
                CategoryDescription = command.CategoryDescription
            };

            var created = await serviceCategoryRepository.AddAsync(serviceCategory);

            return created.ToDto();
        }
    }
}
