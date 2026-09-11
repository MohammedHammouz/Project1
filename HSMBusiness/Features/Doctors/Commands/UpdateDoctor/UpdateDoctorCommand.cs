namespace HSMBusiness.Features.Doctors.Commands.UpdateDoctor
{
    public sealed record UpdateDoctorCommand(
        string Id,
        string DepartmentId,
        string Specialization,
        bool Status,
        string? UserId);
}
