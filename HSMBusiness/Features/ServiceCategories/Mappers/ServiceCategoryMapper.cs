using HSMBusiness.Features.ServiceCategories.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.ServiceCategories.Mappers
{
    public static class ServiceCategoryMapper
    {
        public static ServiceCategoryDto ToDto(this ServiceCategory entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new ServiceCategoryDto
            {
                Id = entity.ID,
                CategoryName = entity.CategoryName,
                CategoryDescription = entity.CategoryDescription
            };
        }

        public static List<ServiceCategoryDto> ToDtos(this IEnumerable<ServiceCategory> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
