namespace HSMBusiness.Features.MedicalServices.Dtos
{
    public class MedicalServiceDto
    {
        public string Id { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public decimal DefaultPrice { get; set; }
        public string? Description { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
