namespace HSMBusiness.Features.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(
        string Name,
        string Role,
        bool Status,
        string Password,
        string EmployeeId);
}
