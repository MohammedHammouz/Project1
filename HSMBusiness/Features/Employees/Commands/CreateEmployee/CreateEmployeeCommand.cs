namespace HSMBusiness.Features.Employees.Commands.CreateEmployee
{
    public sealed record CreateEmployeeCommand(
        string PersonId,
        decimal Salary,
        DateTime HireDate,
        bool IsActive);
}
