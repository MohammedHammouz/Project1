namespace HSMBusiness.Features.Persons.Dtos
{
    public class PersonDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
    }
}
