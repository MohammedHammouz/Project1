using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Employees.Dtos;
using HSMBusiness.Features.Employees.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler(
        IValidator<GetEmployeeByIdQuery> validator,
        EmployeeRepository employeeRepository)
    {
        public async Task<Result<EmployeeDto>> ExecuteAsync(GetEmployeeByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var employee = await employeeRepository.GetByIDAsync(query.Id);
            if (employee is null)
            {
                return EmployeeErrors.NotFound;
            }

            return employee.ToDto();
        }
    }
}
