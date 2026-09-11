namespace HSMBusiness.Features.Bills.Commands.CreateBill
{
    public sealed record CreateBillCommand(
        string PatientId,
        string Status,
        DateTime? Date,
        decimal? PartialPaymentAmount,
        DateTime? DueDate,
        string Invoice,
        decimal GrossAmount,
        decimal? Discount,
        decimal? InsuranceCoverage,
        decimal PatientResponsibility);
}
