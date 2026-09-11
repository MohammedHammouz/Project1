using HSMBusiness.Features.Departments.Dtos;
using HSMBusiness.Features.Departments.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Departments.Queries.GetDepartments
{
    public class GetDepartmentsQueryHandler(DepartmentRepository departmentRepository)
    {
        public async Task<Result<List<DepartmentDto>>> ExecuteAsync(GetDepartmentsQuery query)
        {
            var departments = await departmentRepository.GetAllAsync();

            return departments.ToDtos();
        }
    }
}
