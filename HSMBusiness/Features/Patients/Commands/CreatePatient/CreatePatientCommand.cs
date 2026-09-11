namespace HSMBusiness.Features.Patients.Commands.CreatePatient
{
    public sealed record CreatePatientCommand(
        string MedicalHistory,
        bool Status,
        string PersonId);
}
