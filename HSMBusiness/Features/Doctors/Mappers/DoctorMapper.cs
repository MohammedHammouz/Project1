using HSMBusiness.Features.Doctors.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.Doctors.Mappers
{
    public static class DoctorMapper
    {
        public static DoctorDto ToDto(this Doctor entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new DoctorDto
            {
                Id = entity.ID,
                DepartmentId = entity.DepartmentID,
                Specialization = entity.Specialization,
                Status = entity.Status,
                UserId = entity.UserID
            };
        }

        public static List<DoctorDto> ToDtos(this IEnumerable<Doctor> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
