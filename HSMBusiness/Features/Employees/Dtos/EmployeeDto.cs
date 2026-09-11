namespace HSMBusiness.Features.Employees.Dtos
{
    public class EmployeeDto
    {
        public string Id { get; set; } = string.Empty;
        public string PersonId { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
    }
}
