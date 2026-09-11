using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Patients.Commands.DeletePatient
{
    public class DeletePatientCommandHandler(
        IValidator<DeletePatientCommand> validator,
        PatientRepository patientRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeletePatientCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var patient = await patientRepository.GetByIDAsync(command.Id);
            if (patient is null)
            {
                return PatientErrors.NotFound;
            }

            var isDeleted = await patientRepository.DeleteAsync(patient);
            if (!isDeleted)
            {
                return Error.Failure("Patient_DeleteFailed", "Failed to delete patient.");
            }

            return Result.Deleted;
        }
    }
}
