namespace HSMBusiness.Features.MedicalRecords.Dtos
{
    public class MedicalRecordDto
    {
        public string Id { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Prescriptions { get; set; }
        public string? Status { get; set; }
        public string AuditTrail { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = string.Empty;
    }
}
