namespace HSMBusiness.Features.Departments.Dtos
{
    public class DepartmentDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? HeadOf { get; set; }
    }
}
