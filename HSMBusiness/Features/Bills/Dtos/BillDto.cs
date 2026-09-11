namespace HSMBusiness.Features.Bills.Dtos
{
    public class BillDto
    {
        public string Id { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public decimal? PartialPaymentAmount { get; set; }
        public DateTime? DueDate { get; set; }
        public string Invoice { get; set; } = string.Empty;
        public decimal GrossAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? InsuranceCoverage { get; set; }
        public decimal PatientResponsibility { get; set; }
    }
}
