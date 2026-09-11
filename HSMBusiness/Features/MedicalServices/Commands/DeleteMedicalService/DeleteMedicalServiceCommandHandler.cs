using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalServices.Commands.DeleteMedicalService
{
    public class DeleteMedicalServiceCommandHandler(
        IValidator<DeleteMedicalServiceCommand> validator,
        MedicalServiceRepository medicalServiceRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteMedicalServiceCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var medicalService = await medicalServiceRepository.GetByIDAsync(command.Id);
            if (medicalService is null)
            {
                return MedicalServiceErrors.NotFound;
            }

            var isDeleted = await medicalServiceRepository.DeleteAsync(medicalService);
            if (!isDeleted)
            {
                return Error.Failure("MedicalService_DeleteFailed", "Failed to delete medical service.");
            }

            return Result.Deleted;
        }
    }
}
