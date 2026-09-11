using HSMBusiness.Features.Departments.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.Departments.Mappers
{
    public static class DepartmentMapper
    {
        public static DepartmentDto ToDto(this Department entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new DepartmentDto
            {
                Id = entity.ID,
                Name = entity.Name,
                HeadOf = entity.HeadOf
            };
        }

        public static List<DepartmentDto> ToDtos(this IEnumerable<Department> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
