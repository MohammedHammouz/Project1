namespace HSMBusiness.Features.MedicalServices.Commands.UpdateMedicalService
{
    public sealed record UpdateMedicalServiceCommand(
        string Id,
        string ServiceName,
        string CategoryId,
        decimal DefaultPrice,
        string? Description,
        int? DurationMinutes);
}
