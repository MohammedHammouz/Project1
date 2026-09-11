using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler(
        IValidator<DeleteEmployeeCommand> validator,
        EmployeeRepository employeeRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteEmployeeCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var employee = await employeeRepository.GetByIDAsync(command.Id);
            if (employee is null)
            {
                return EmployeeErrors.NotFound;
            }

            var isDeleted = await employeeRepository.DeleteAsync(employee);
            if (!isDeleted)
            {
                return Error.Failure("Employee_DeleteFailed", "Failed to delete employee.");
            }

            return Result.Deleted;
        }
    }
}
