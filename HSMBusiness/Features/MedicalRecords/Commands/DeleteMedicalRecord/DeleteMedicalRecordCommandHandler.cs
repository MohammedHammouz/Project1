using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalRecords.Commands.DeleteMedicalRecord
{
    public class DeleteMedicalRecordCommandHandler(
        IValidator<DeleteMedicalRecordCommand> validator,
        MedicalRecordRepository medicalRecordRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteMedicalRecordCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var medicalRecord = await medicalRecordRepository.GetByIDAsync(command.Id);
            if (medicalRecord is null)
            {
                return MedicalRecordErrors.NotFound;
            }

            var isDeleted = await medicalRecordRepository.DeleteAsync(medicalRecord);
            if (!isDeleted)
            {
                return Error.Failure("MedicalRecord_DeleteFailed", "Failed to delete medical record.");
            }

            return Result.Deleted;
        }
    }
}
