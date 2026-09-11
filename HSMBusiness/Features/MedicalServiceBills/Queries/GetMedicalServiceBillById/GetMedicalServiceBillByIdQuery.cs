namespace HSMBusiness.Features.MedicalServiceBills.Queries.GetMedicalServiceBillById
{
    public sealed record GetMedicalServiceBillByIdQuery(
        string ServiceId,
        string BillId);
}
