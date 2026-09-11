using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.ServiceCategories.Commands.DeleteServiceCategory
{
    public class DeleteServiceCategoryCommandHandler(
        IValidator<DeleteServiceCategoryCommand> validator,
        ServicesCategoryRepository serviceCategoryRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteServiceCategoryCommand command)
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

            var isDeleted = await serviceCategoryRepository.DeleteAsync(serviceCategory);
            if (!isDeleted)
            {
                return Error.Failure("ServiceCategory_DeleteFailed", "Failed to delete service category.");
            }

            return Result.Deleted;
        }
    }
}
