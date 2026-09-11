using HSMBusiness.Features.Employees.Dtos;
using HSMBusiness.Features.Employees.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Employees.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler(EmployeeRepository employeeRepository)
    {
        public async Task<Result<List<EmployeeDto>>> ExecuteAsync(GetEmployeesQuery query)
        {
            var employees = await employeeRepository.GetAllAsync();

            return employees.ToDtos();
        }
    }
}
