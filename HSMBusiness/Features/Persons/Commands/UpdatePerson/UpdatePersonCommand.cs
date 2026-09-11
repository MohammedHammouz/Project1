namespace HSMBusiness.Features.Persons.Commands.UpdatePerson
{
    public sealed record UpdatePersonCommand(
        string Id,
        string Name,
        string ContactNumber,
        string Email,
        string Gender,
        string Address,
        DateOnly DateOfBirth);
}
