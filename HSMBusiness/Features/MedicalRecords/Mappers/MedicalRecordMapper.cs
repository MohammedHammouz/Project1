using HSMBusiness.Features.MedicalRecords.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.MedicalRecords.Mappers
{
    public static class MedicalRecordMapper
    {
        public static MedicalRecordDto ToDto(this MedicalRecord entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new MedicalRecordDto
            {
                Id = entity.ID,
                PatientId = entity.PatientID,
                Diagnosis = entity.Diagnosis,
                Treatment = entity.Treatment,
                Prescriptions = entity.Prescriptions,
                Status = entity.Status,
                AuditTrail = entity.AuditTrail,
                AccessLevel = entity.AccessLevel
            };
        }

        public static List<MedicalRecordDto> ToDtos(this IEnumerable<MedicalRecord> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
