using HSMBusiness.Features.MedicalServices.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.MedicalServices.Mappers
{
    public static class MedicalServiceMapper
    {
        public static MedicalServiceDto ToDto(this MedicalService entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new MedicalServiceDto
            {
                Id = entity.ID,
                ServiceName = entity.ServiceName,
                CategoryId = entity.CategoryID,
                DefaultPrice = entity.DefaultPrice,
                Description = entity.Description,
                DurationMinutes = entity.DurationMinutes
            };
        }

        public static List<MedicalServiceDto> ToDtos(this IEnumerable<MedicalService> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
