using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler(
        IValidator<DeleteDepartmentCommand> validator,
        DepartmentRepository departmentRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteDepartmentCommand command)
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

            var isDeleted = await departmentRepository.DeleteAsync(department);
            if (!isDeleted)
            {
                return Error.Failure("Department_DeleteFailed", "Failed to delete department.");
            }

            return Result.Deleted;
        }
    }
}
