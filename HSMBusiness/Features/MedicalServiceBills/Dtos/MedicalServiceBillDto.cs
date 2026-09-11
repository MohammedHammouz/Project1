namespace HSMBusiness.Features.MedicalServiceBills.Dtos
{
    public class MedicalServiceBillDto
    {
        public string ServiceId { get; set; } = string.Empty;
        public string BillId { get; set; } = string.Empty;
        public int? Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
