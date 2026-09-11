using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Doctors.Dtos;
using HSMBusiness.Features.Doctors.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Doctors.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler(
        IValidator<CreateDoctorCommand> validator,
        DoctorRepository doctorRepository,
        DepartmentRepository departmentRepository,
        UserRepository userRepository)
    {
        public async Task<Result<DoctorDto>> ExecuteAsync(CreateDoctorCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
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

            var doctor = new Doctor
            {
                DepartmentID = command.DepartmentId,
                Specialization = command.Specialization,
                Status = command.Status,
                UserID = command.UserId
            };

            var created = await doctorRepository.AddAsync(doctor);

            return created.ToDto();
        }
    }
}
