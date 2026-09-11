namespace HSMBusiness.Features.Patients.Commands.UpdatePatient
{
    public sealed record UpdatePatientCommand(
        string Id,
        string MedicalHistory,
        bool Status,
        string PersonId);
}
