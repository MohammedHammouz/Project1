namespace HSMBusiness.Features.MedicalServices.Commands.CreateMedicalService
{
    public sealed record CreateMedicalServiceCommand(
        string ServiceName,
        string CategoryId,
        decimal DefaultPrice,
        string? Description,
        int? DurationMinutes);
}
