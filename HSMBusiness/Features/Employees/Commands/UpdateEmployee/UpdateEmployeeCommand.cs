namespace HSMBusiness.Features.Employees.Commands.UpdateEmployee
{
    public sealed record UpdateEmployeeCommand(
        string Id,
        string PersonId,
        decimal Salary,
        DateTime HireDate,
        bool IsActive);
}
