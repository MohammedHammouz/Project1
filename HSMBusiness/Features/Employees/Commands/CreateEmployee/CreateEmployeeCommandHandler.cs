using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Employees.Dtos;
using HSMBusiness.Features.Employees.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler(
        IValidator<CreateEmployeeCommand> validator,
        EmployeeRepository employeeRepository,
        PersonRepository personRepository)
    {
        public async Task<Result<EmployeeDto>> ExecuteAsync(CreateEmployeeCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var person = await personRepository.GetByIDAsync(command.PersonId);
            if (person is null)
            {
                return EmployeeErrors.PersonNotFound;
            }

            var employee = new Employee
            {
                PersonID = command.PersonId,
                Salary = command.Salary,
                HireDate = command.HireDate,
                IsActive = command.IsActive
            };

            var created = await employeeRepository.AddAsync(employee);

            return created.ToDto();
        }
    }
}
