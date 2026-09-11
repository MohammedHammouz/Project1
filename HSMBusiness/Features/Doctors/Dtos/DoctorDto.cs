namespace HSMBusiness.Features.Doctors.Dtos
{
    public class DoctorDto
    {
        public string Id { get; set; } = string.Empty;
        public string DepartmentId { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string? UserId { get; set; }
    }
}
