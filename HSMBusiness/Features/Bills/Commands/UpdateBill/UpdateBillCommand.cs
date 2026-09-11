namespace HSMBusiness.Features.Bills.Commands.UpdateBill
{
    public sealed record UpdateBillCommand(
        string Id,
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
