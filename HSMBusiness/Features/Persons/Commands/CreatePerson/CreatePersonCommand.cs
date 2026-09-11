namespace HSMBusiness.Features.Persons.Commands.CreatePerson
{
    public sealed record CreatePersonCommand(
        string Name,
        string ContactNumber,
        string Email,
        string Gender,
        string Address,
        DateOnly DateOfBirth);
}
