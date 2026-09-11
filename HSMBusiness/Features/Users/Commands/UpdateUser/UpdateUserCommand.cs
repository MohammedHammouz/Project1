namespace HSMBusiness.Features.Users.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(
        string Id,
        string Name,
        string Role,
        bool Status,
        string Password,
        string EmployeeId);
}
