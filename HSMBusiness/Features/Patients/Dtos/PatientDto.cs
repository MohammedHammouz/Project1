namespace HSMBusiness.Features.Patients.Dtos
{
    public class PatientDto
    {
        public string Id { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string PersonId { get; set; } = string.Empty;
    }
}
