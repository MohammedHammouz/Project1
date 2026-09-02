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

        public AuditLogRepository auditLogRepository
        {
            get { return _auditLog; }
        }

        public AuditLogService(AuditLogRepository auditLog, enMode mode = enMode.Add)
        {
            _auditLog = auditLog;
            _mode = mode;
        }

        private async Task<bool> Add(AuditLogDTO auditLogDTO)
        {
            var auditLogEntity = new AuditLogMapper().ToEntity(auditLogDTO);

            var AddNew = await _auditLog.AddAsync(auditLogEntity);

            auditLogEntity.ID = AddNew.ID;

            return auditLogEntity.ID != "";
        }

        private async Task<(int, string?, bool)> Update(string ID,AuditLogDTO auditLogDTO)
        {
            var CurrentAuditLog = await _auditLog.GetByIDAsync(ID);

            var response = await resultPattern.GiveResponse(200);

            if (CurrentAuditLog == null)
            {
                response = await resultPattern.GiveResponse(404);

                return (response.Status, response.Response, response.IsSuccess);
            }

            CurrentAuditLog = new AuditLogMapper().ToEntity( auditLogDTO, AuditLogMapper.enMode.Update);

            return ( response.Status, response.Response, await _auditLog.UpdateAsync(CurrentAuditLog));
        }

        public async Task<(int, string?, bool)> Save(AuditLogDTO auditLogDTO,string ID = "")
        {
            var response = await resultPattern.GiveResponse(200);

            switch (_mode)
            {
                case enMode.Add:

                    if (await Add(auditLogDTO))
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response =
                            await resultPattern.GiveResponse(400);
                    }

                    return ( response.Status,response.Response,response.IsSuccess);

                case enMode.Update:

                    return await Update(ID, auditLogDTO);
            }

            response =await resultPattern.GiveResponse(500);

            return ( response.Status, response.Response,  response.IsSuccess);
        }

        public async Task<(int,string?,bool,List<AuditLogDTO>)> GetAll()
        {
            var auditLogs = await _auditLog.GetAllAsync();

            var response = await resultPattern.GiveResponse(200);

            if (auditLogs == null)
            {
                response = await resultPattern.GiveResponse(404);

                return ( response.Status, response.Response, response.IsSuccess, null);
            }

            return ( response.Status, response.Response,  response.IsSuccess, auditLogs.Select(a =>
                    new AuditLogDTO( a.ID,a.UserID,a.Entity,a.Action,a.Timestamp,a.Details)).ToList());
        }

        public async Task<(int,string?,bool, AuditLogDTO)> GetByID(string ID)
        {
            var auditLog = await _auditLog.GetByIDAsync(ID);

            var response = await resultPattern.GiveResponse(200);

            if (auditLog == null)
            {
                response = await resultPattern.GiveResponse(404);

                return ( response.Status, response.Response, response.IsSuccess, new AuditLogDTO( "", "", "", "", DateTime.Now, null ));
            }

            return (response.Status,response.Response,response.IsSuccess,new AuditLogDTO(auditLog.ID,auditLog.UserID,auditLog.Entity,auditLog.Action,auditLog.Timestamp,auditLog.Details));
        }

        public async Task<(int, string?, bool)> Delete(
            string ID)
        {
            var auditLog =await _auditLog.GetByIDAsync(ID);

            var response =await resultPattern.GiveResponse(200);

            if (auditLog == null)
            {
                response =await resultPattern.GiveResponse(404);

                return (response.Status,response.Response,response.IsSuccess);
            }

            bool IsDeleted =await _auditLog.DeleteAsync(auditLog);

            if (!IsDeleted)
            {
                response =await resultPattern.GiveResponse(400);

                return (response.Status,response.Response,response.IsSuccess
                );
            }

            return (response.Status,response.Response,response.IsSuccess);
        }
    }
}