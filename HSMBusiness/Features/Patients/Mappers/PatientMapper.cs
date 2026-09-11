using HSMBusiness.Features.Patients.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.Patients.Mappers
{
    public static class PatientMapper
    {
        public static PatientDto ToDto(this Patient entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new PatientDto
            {
                Id = entity.ID,
                MedicalHistory = entity.MedicalHistory,
                Status = entity.Status,
                PersonId = entity.PersonID
            };
        }

        public static List<PatientDto> ToDtos(this IEnumerable<Patient> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
