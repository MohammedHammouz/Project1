namespace HSMBusiness.Features.Doctors.Commands.CreateDoctor
{
    public sealed record CreateDoctorCommand(
        string DepartmentId,
        string Specialization,
        bool Status,
        string? UserId);
}
