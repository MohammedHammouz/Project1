using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Doctors.Dtos;
using HSMBusiness.Features.Doctors.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Doctors.Commands.UpdateDoctor
{
    public class UpdateDoctorCommandHandler(
        IValidator<UpdateDoctorCommand> validator,
        DoctorRepository doctorRepository,
        DepartmentRepository departmentRepository,
        UserRepository userRepository)
    {
        public async Task<Result<DoctorDto>> ExecuteAsync(UpdateDoctorCommand command)
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

            var department = await departmentRepository.GetByIDAsync(command.DepartmentId);
            if (department is null)
            {
                return DoctorErrors.DepartmentNotFound;
            }

            if (!string.IsNullOrWhiteSpace(command.UserId))
            {
                var user = await userRepository.GetByIDAsync(command.UserId);
                if (user is null)
                {
                    return DoctorErrors.UserNotFound;
                }
            }

            doctor.DepartmentID = command.DepartmentId;
            doctor.Specialization = command.Specialization;
            doctor.Status = command.Status;
            doctor.UserID = command.UserId;

            await doctorRepository.UpdateAsync(doctor);

            return doctor.ToDto();
        }
    }
}
