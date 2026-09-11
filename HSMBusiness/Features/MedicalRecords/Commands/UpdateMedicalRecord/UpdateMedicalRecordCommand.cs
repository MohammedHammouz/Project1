namespace HSMBusiness.Features.MedicalRecords.Commands.UpdateMedicalRecord
{
    public sealed record UpdateMedicalRecordCommand(
        string Id,
        string PatientId,
        string? Diagnosis,
        string? Treatment,
        string? Prescriptions,
        string? Status,
        string AuditTrail,
        string AccessLevel);
}
