using HSMBusiness.Features.Employees.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.Employees.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto ToDto(this Employee entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new EmployeeDto
            {
                Id = entity.ID,
                PersonId = entity.PersonID,
                Salary = entity.Salary,
                HireDate = entity.HireDate,
                IsActive = entity.IsActive
            };
        }

        public static List<EmployeeDto> ToDtos(this IEnumerable<Employee> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
