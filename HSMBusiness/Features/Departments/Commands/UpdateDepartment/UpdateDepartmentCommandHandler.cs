using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Departments.Dtos;
using HSMBusiness.Features.Departments.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler(
        IValidator<UpdateDepartmentCommand> validator,
        DepartmentRepository departmentRepository)
    {
        public async Task<Result<DepartmentDto>> ExecuteAsync(UpdateDepartmentCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var department = await departmentRepository.GetByIDAsync(command.Id);
            if (department is null)
            {
                return DepartmentErrors.NotFound;
            }

            department.Name = command.Name;
            department.HeadOf = command.HeadOf;

            await departmentRepository.UpdateAsync(department);

            return department.ToDto();
        }
    }
}
