namespace HSMBusiness.Features.Departments.Commands.CreateDepartment
{
    public sealed record CreateDepartmentCommand(
        string Name,
        string? HeadOf);
}
