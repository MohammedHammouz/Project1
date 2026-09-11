namespace HSMBusiness.Features.Departments.Commands.UpdateDepartment
{
    public sealed record UpdateDepartmentCommand(
        string Id,
        string Name,
        string? HeadOf);
}
