using HSMBusiness.Features.Reports.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.Reports.Mappers
{
    public static class ReportMapper
    {
        public static ReportDto ToDto(this Report entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new ReportDto
            {
                Id = entity.ID,
                Type = entity.Type,
                GeneratedOn = entity.GeneratedOn,
                GeneratedBy = entity.GeneratedBy,
                AppointmentCount = entity.AppointmentCount,
                Revenue = entity.Revenue,
                PaymentsReceived = entity.PaymentsReceived,
                PendingPayments = entity.PendingPayments,
                Metrics = entity.Metrics,
                ExportFormat = entity.ExportFormat,
                Status = entity.Status,
                Notes = entity.Notes
            };
        }

        public static List<ReportDto> ToDtos(this IEnumerable<Report> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
