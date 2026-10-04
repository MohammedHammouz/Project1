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
    public class AuditLogService
    {
        ResultPatern resultPattern = new ResultPatern();

        public enum enMode { Add = 0, Update }

        public enMode _mode = enMode.Add;

        private readonly AuditLogRepository _auditLog;
        private readonly UserService _userService;
        public AuditLogRepository auditLogRepository
        {
            get { return _auditLog; }
        }

        public AuditLogService(AuditLogRepository auditLog,UserService userService, enMode mode = enMode.Add)
        {
            _auditLog = auditLog;
            _userService = userService;
            _mode = mode;
        }

        private async Task<(int Status,bool IsSuccess)> Add(AuditLogDTO auditLogDTO ,int Status)
        {
            var auditLogEntity = new AuditLogMapper().ToEntity(auditLogDTO);
            if(!await _userService.IsUserExist(auditLogEntity.UserID))
            {
                Status = 404;
                return (Status,false);
            }
            if(auditLogDTO.Action!= "Delete" || auditLogDTO.Action != "Access" || auditLogDTO.Action != "Create" || auditLogDTO.Action != "Update" ){
                Status = 500;
                return (Status,false);
            }
            var AddNew = await _auditLog.AddAsync(auditLogEntity);

            auditLogEntity.ID = AddNew.ID;
            
            return (Status,auditLogEntity.ID != "");
        }

        private async Task<(int, string?, bool)> Update(string ID,AuditLogDTO auditLogDTO)
        {
            var CurrentAuditLog = await _auditLog.GetByIDAsync(ID);
            
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (!await _userService.IsUserExist(CurrentAuditLog.UserID))
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return (response.Status, response.Response, response.IsSuccess);
            }
            if (CurrentAuditLog == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return (response.Status, response.Response, response.IsSuccess);
            }

            CurrentAuditLog = new AuditLogMapper().ToEntity( auditLogDTO, AuditLogMapper.enMode.Update);

            return ( response.Status, response.Response, await _auditLog.UpdateAsync(CurrentAuditLog));
        }

        public async Task<(int Status, string?Response, bool IsSuccess)> Save(AuditLogDTO auditLogDTO,string ID = "")
        {
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            switch (_mode)
            {
                case enMode.Add:
                    var addNew = await Add(auditLogDTO, response.Status);
                    if (addNew.IsSuccess)
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response =
                            await resultPattern.GiveResponse(addNew.Status, new Dictionary<string, string>());
                    }

                    return ( response.Status,response.Response,response.IsSuccess);

                case enMode.Update:

                    return await Update(ID, auditLogDTO);
            }

            response =await resultPattern.GiveResponse(500, new Dictionary<string, string>());

            return ( response.Status, response.Response,  response.IsSuccess);
        }

        public async Task<(int Status,string? Response,bool IsSuccess,List<AuditLogDTO>listOfAuditLogs)> GetAll()
        {
            var auditLogs = await _auditLog.GetAllAsync();

            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            if (auditLogs == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return ( response.Status, response.Response, response.IsSuccess, null);
            }

            return ( response.Status, response.Response,  response.IsSuccess, auditLogs.Select(a =>
                    new AuditLogDTO( a.ID,a.UserID,a.Entity,a.Action,a.Timestamp,a.Details)).ToList());
        }

        public async Task<(int Status,string? Response,bool IsSuccess, AuditLogDTO auditLogDTO1)> GetByID(string ID)
        {
            var auditLog = await _auditLog.GetByIDAsync(ID);

            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            if (auditLog == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return ( response.Status, response.Response, response.IsSuccess, new AuditLogDTO( "", "", "", "", DateTime.Now, null ));
            }

            return (response.Status,response.Response,response.IsSuccess,new AuditLogDTO(auditLog.ID,auditLog.UserID,auditLog.Entity,auditLog.Action,auditLog.Timestamp,auditLog.Details));
        }

        public async Task<(int Status, string?Response, bool IsSuccess)> Delete(
            string ID)
        {
            var auditLog =await _auditLog.GetByIDAsync(ID);

            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            if (auditLog == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return (response.Status,response.Response,response.IsSuccess);
            }

            bool IsDeleted =await _auditLog.DeleteAsync(auditLog);

            if (!IsDeleted)
            {
                response =await resultPattern.GiveResponse(400, new Dictionary<string, string>());

                return (response.Status,response.Response,response.IsSuccess
                );
            }

            return (response.Status,response.Response,response.IsSuccess);
        }
    }
}