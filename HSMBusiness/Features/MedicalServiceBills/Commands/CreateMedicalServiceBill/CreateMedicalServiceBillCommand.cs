namespace HSMBusiness.Features.MedicalServiceBills.Commands.CreateMedicalServiceBill
{
    public sealed record CreateMedicalServiceBillCommand(
        string ServiceId,
        string BillId,
        int? Quantity,
        decimal UnitPrice,
        decimal? TotalPrice);
}
