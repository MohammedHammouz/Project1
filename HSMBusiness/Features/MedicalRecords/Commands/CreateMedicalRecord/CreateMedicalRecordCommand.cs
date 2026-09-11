namespace HSMBusiness.Features.MedicalRecords.Commands.CreateMedicalRecord
{
    public sealed record CreateMedicalRecordCommand(
        string PatientId,
        string? Diagnosis,
        string? Treatment,
        string? Prescriptions,
        string? Status,
        string AuditTrail,
        string AccessLevel);
}
