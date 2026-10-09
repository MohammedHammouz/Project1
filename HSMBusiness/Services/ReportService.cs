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
        private readonly UserService _userService;
        public ReportRepository reportRepository
        {
            get { return _report; }
        }
        public ReportService(ReportRepository report, UserService userService, enMode mode = enMode.Add)
        {
            _report = report;
            _userService = userService;
            _mode = mode;
        }
        private async Task<(int Status, bool IsSuccess)> Add(ReportDTO reportDTO)
        {
            var reportEntity = new ReportMapper().ToEntity(reportDTO);
            if (!await _userService.IsUserExist(reportDTO.GeneratedBy))
            {
                return (500, false);
            }
            if (reportDTO.ExportFormat != "PDF" && reportDTO.ExportFormat!="Excel")
            {
                return (500, false);
            }
            var AddNew = await _report.AddAsync(reportEntity);
            if (AddNew.ID == "")
            {
                return (500, false);
            }
            reportEntity.ID = AddNew.ID;
            return (200,reportEntity.ID != "");
        }
        private async Task<(int Status, string?Response, bool IsSuccess)> Update(string ID,ReportDTO reportDTO)
        {
            var CurrentReport = await _report.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (!await _userService.IsUserExist(reportDTO.GeneratedBy))
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            if (CurrentReport == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return ( response.Status, response.Response,  response.IsSuccess) ;
            }
            if (reportDTO.ExportFormat != "PDF" && reportDTO.ExportFormat != "Excel")
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            CurrentReport =new ReportMapper().ToEntity(reportDTO,CurrentReport,ReportMapper.enMode.Update);
            return (response.Status, response.Response,await _report.UpdateAsync(CurrentReport));
        }
        public async Task<(int Status, string?Response, bool IsSuccess)> Save(ReportDTO reportDTO,string ID = "")
        {
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            switch (_mode)
            {
                case enMode.Add:
                    var AddNew = await Add(reportDTO);
                    if (AddNew.IsSuccess)
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response =await resultPattern.GiveResponse(400, new Dictionary<string, string>());
                    }
                    return (response.Status,response.Response,response.IsSuccess);
                case enMode.Update:
                    return await Update(ID, reportDTO);
            }
            response =await resultPattern.GiveResponse(500, new Dictionary<string, string>());
            return (response.Status,response.Response,response.IsSuccess);
        }
        public async Task<(int Status,string?Response,bool IsSuccess,List<ReportDTO>? ListOfReports)> GetAll()
        {
            var reports = await _report.GetAllAsync();
            var response =  await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (reports == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess,null);
            }
            return (response.Status,response.Response,response.IsSuccess,reports.Select(r => new ReportMapper().ToDTO(r)).ToList());
        }
        public async Task<( int Status, string?Response,  bool IsSuccess,  ReportDTO? reportDTO1)> GetByID(string ID)
        {
            var report = await _report.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (report == null)
            {
                response =  await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return ( response.Status, response.Response, response.IsSuccess,  null);
            }
            return (response.Status,response.Response,response.IsSuccess,new ReportMapper().ToDTO(report));
        }
        public async Task<(int Status, string?Response, bool IsSuccess)> Delete(string ID)
        {
            var report =await _report.GetByIDAsync(ID);
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (report == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess);
            }
            bool IsDeleted = await _report.DeleteAsync(report);
            if (!IsDeleted)
            {
                response =await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess);
            }
            return (response.Status,response.Response,response.IsSuccess);
        }
    }
}