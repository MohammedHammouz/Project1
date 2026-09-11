using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Departments.Dtos;
using HSMBusiness.Features.Departments.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQueryHandler(
        IValidator<GetDepartmentByIdQuery> validator,
        DepartmentRepository departmentRepository)
    {
        public async Task<Result<DepartmentDto>> ExecuteAsync(GetDepartmentByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var department = await departmentRepository.GetByIDAsync(query.Id);
            if (department is null)
            {
                return DepartmentErrors.NotFound;
            }

            return department.ToDto();
        }
    }
}
