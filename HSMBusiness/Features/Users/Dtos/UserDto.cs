namespace HSMBusiness.Features.Users.Dtos
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
    }
}
