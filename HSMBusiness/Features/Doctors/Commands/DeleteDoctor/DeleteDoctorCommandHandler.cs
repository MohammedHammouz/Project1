using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Doctors.Commands.DeleteDoctor
{
    public class DeleteDoctorCommandHandler(
        IValidator<DeleteDoctorCommand> validator,
        DoctorRepository doctorRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteDoctorCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var doctor = await doctorRepository.GetByIDAsync(command.Id);
            if (doctor is null)
            {
                return DoctorErrors.NotFound;
            }

            var isDeleted = await doctorRepository.DeleteAsync(doctor);
            if (!isDeleted)
            {
                return Error.Failure("Doctor_DeleteFailed", "Failed to delete doctor.");
            }

            return Result.Deleted;
        }
    }
}
