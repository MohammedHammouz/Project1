using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Employees.Dtos;
using HSMBusiness.Features.Employees.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler(
        IValidator<UpdateEmployeeCommand> validator,
        EmployeeRepository employeeRepository,
        PersonRepository personRepository)
    {
        public async Task<Result<EmployeeDto>> ExecuteAsync(UpdateEmployeeCommand command)
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

            var person = await personRepository.GetByIDAsync(command.PersonId);
            if (person is null)
            {
                return EmployeeErrors.PersonNotFound;
            }

            employee.PersonID = command.PersonId;
            employee.Salary = command.Salary;
            employee.HireDate = command.HireDate;
            employee.IsActive = command.IsActive;

            await employeeRepository.UpdateAsync(employee);

            return employee.ToDto();
        }
    }
}
