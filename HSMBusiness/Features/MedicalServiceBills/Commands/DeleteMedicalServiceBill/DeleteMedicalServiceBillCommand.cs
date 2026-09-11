namespace HSMBusiness.Features.MedicalServiceBills.Commands.DeleteMedicalServiceBill
{
    public sealed record DeleteMedicalServiceBillCommand(
        string ServiceId,
        string BillId);
}
