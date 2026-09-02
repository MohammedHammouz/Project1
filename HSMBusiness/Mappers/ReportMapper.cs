using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class ReportMapper
    {
        public enum enMode { Add, Update }

        public Report ToEntity(
            ReportDTO reportDTO,
            enMode mode = enMode.Add)
        {
            if (mode == enMode.Add)
            {
                return new Report
                {
                    ID = reportDTO.ID,
                    Type = reportDTO.Type,
                    GeneratedOn = reportDTO.GeneratedOn,
                    GeneratedBy = reportDTO.GeneratedBy,
                    AppointmentCount = reportDTO.AppointmentCount,
                    Revenue = reportDTO.Revenue,
                    PaymentsReceived = reportDTO.PaymentsReceived,
                    PendingPayments = reportDTO.PendingPayments,
                    Metrics = reportDTO.Metrics,
                    ExportFormat = reportDTO.ExportFormat,
                    Status = reportDTO.Status,
                    Notes = reportDTO.Notes
                };
            }
            else
            {
                return new Report
                {
                    ID = reportDTO.ID,
                    Type = reportDTO.Type,
                    GeneratedOn = reportDTO.GeneratedOn,
                    GeneratedBy = reportDTO.GeneratedBy,
                    AppointmentCount = reportDTO.AppointmentCount,
                    Revenue = reportDTO.Revenue,
                    PaymentsReceived = reportDTO.PaymentsReceived,
                    PendingPayments = reportDTO.PendingPayments,
                    Metrics = reportDTO.Metrics,
                    ExportFormat = reportDTO.ExportFormat,
                    Status = reportDTO.Status,
                    Notes = reportDTO.Notes
                };
            }
        }

        public ReportDTO ToDTO(Report report)
        {
            return new ReportDTO
            (
                report.ID,
                report.Type,
                report.GeneratedOn,
                report.GeneratedBy,
                report.AppointmentCount,
                report.Revenue,
                report.PaymentsReceived,
                report.PendingPayments,
                report.Metrics,
                report.ExportFormat,
                report.Status,
                report.Notes
            );
        }
    }
}