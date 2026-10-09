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
            ReportDTO reportDTO,Report report=null,
            enMode mode = enMode.Add)
        {
            if (mode == enMode.Add)
            {
                return new Report
                {
                    
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

                
                report.Type = reportDTO.Type;
                report.GeneratedOn = reportDTO.GeneratedOn;
                report.GeneratedBy = reportDTO.GeneratedBy;
                report.AppointmentCount = reportDTO.AppointmentCount;
                report.Revenue = reportDTO.Revenue;
                report.PaymentsReceived = reportDTO.PaymentsReceived;
                report.PendingPayments = reportDTO.PendingPayments;
                report.Metrics = reportDTO.Metrics;
                report.ExportFormat = reportDTO.ExportFormat;
                report.Status = reportDTO.Status;
                report.Notes = reportDTO.Notes;
            }
            return report;
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