using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
using HSMDataAccess.DTOs;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Services
{
    public class ReportService
    {
        ResultPatern resultPattern = new ResultPatern();

        public enum enMode { Add = 0, Update }

        public enMode _mode = enMode.Add;

        private readonly ReportRepository _report;

        public ReportRepository reportRepository
        {
            get { return _report; }
        }

        public ReportService(
            ReportRepository report,
            enMode mode = enMode.Add)
        {
            _report = report;
            _mode = mode;
        }

        private async Task<bool> Add(ReportDTO reportDTO)
        {
            var reportEntity =
                new ReportMapper().ToEntity(reportDTO);

            var AddNew =
                await _report.AddAsync(reportEntity);

            reportEntity.ID = AddNew.ID;

            return reportEntity.ID != "";
        }

        private async Task<(int, string?, bool)> Update(
            string ID,
            ReportDTO reportDTO)
        {
            var CurrentReport =
                await _report.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (CurrentReport == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            CurrentReport =
                new ReportMapper().ToEntity(
                    reportDTO,
                    ReportMapper.enMode.Update
                );

            return (
                response.Status,
                response.Response,
                await _report.UpdateAsync(CurrentReport)
            );
        }

        public async Task<(int, string?, bool)> Save(
            ReportDTO reportDTO,
            string ID = "")
        {
            var response =
                await resultPattern.GiveResponse(200);

            switch (_mode)
            {
                case enMode.Add:

                    if (await Add(reportDTO))
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response =
                            await resultPattern.GiveResponse(400);
                    }

                    return (
                        response.Status,
                        response.Response,
                        response.IsSuccess
                    );

                case enMode.Update:

                    return await Update(ID, reportDTO);
            }

            response =
                await resultPattern.GiveResponse(500);

            return (
                response.Status,
                response.Response,
                response.IsSuccess
            );
        }

        public async Task<(
            int,
            string?,
            bool,
            List<ReportDTO>
        )> GetAll()
        {
            var reports =
                await _report.GetAllAsync();

            var response =
                await resultPattern.GiveResponse(200);

            if (reports == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    null
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess,
                reports.Select(r =>
                    new ReportDTO(
                        r.ID,
                        r.Type,
                        r.GeneratedOn,
                        r.GeneratedBy,
                        r.AppointmentCount,
                        r.Revenue,
                        r.PaymentsReceived,
                        r.PendingPayments,
                        r.Metrics,
                        r.ExportFormat,
                        r.Status,
                        r.Notes
                    )
                ).ToList()
            );
        }

        public async Task<(
            int,
            string?,
            bool,
            ReportDTO
        )> GetByID(string ID)
        {
            var report =
                await _report.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (report == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    new ReportDTO(
                        "",
                        "",
                        DateTime.Now,
                        "",
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
                    )
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess,
                new ReportDTO(
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
                )
            );
        }

        public async Task<(int, string?, bool)> Delete(string ID)
        {
            var report =
                await _report.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (report == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            bool IsDeleted =
                await _report.DeleteAsync(report);

            if (!IsDeleted)
            {
                response =
                    await resultPattern.GiveResponse(400);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess
            );
        }
    }
}