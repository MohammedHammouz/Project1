using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Features.Departments.Dtos;
using HSMBusiness.Features.Departments.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler(
        IValidator<CreateDepartmentCommand> validator,
        DepartmentRepository departmentRepository)
    {
        public async Task<Result<DepartmentDto>> ExecuteAsync(CreateDepartmentCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var department = new Department
            {
                Name = command.Name,
                HeadOf = command.HeadOf
            };

            var newDepartment = await departmentRepository.AddAsync(department);

            return newDepartment.ToDto();
        }
    }
}
