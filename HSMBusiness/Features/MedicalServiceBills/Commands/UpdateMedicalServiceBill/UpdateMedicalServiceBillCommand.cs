namespace HSMBusiness.Features.MedicalServiceBills.Commands.UpdateMedicalServiceBill
{
    public sealed record UpdateMedicalServiceBillCommand(
        string ServiceId,
        string BillId,
        int? Quantity,
        decimal UnitPrice,
        decimal? TotalPrice);
}
