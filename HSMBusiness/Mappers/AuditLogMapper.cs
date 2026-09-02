using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class AuditLogMapper
    {
        public enum enMode { Add, Update }

        public AuditLog ToEntity(AuditLogDTO auditLogDTO,enMode mode = enMode.Add)
        {
            if (mode == enMode.Add)
            {
                return new AuditLog
                {
                    ID = auditLogDTO.ID,
                    UserID = auditLogDTO.UserID,
                    Entity = auditLogDTO.Entity,
                    Action = auditLogDTO.Action,
                    Timestamp = auditLogDTO.Timestamp,
                    Details = auditLogDTO.Details
                };
            }
            else
            {
                return new AuditLog
                {
                    ID = auditLogDTO.ID,
                    UserID = auditLogDTO.UserID,
                    Entity = auditLogDTO.Entity,
                    Action = auditLogDTO.Action,
                    Timestamp = auditLogDTO.Timestamp,
                    Details = auditLogDTO.Details
                };
            }
        }

        public AuditLogDTO ToDTO(AuditLog auditLog)
        {
            return new AuditLogDTO
            (
                auditLog.ID,
                auditLog.UserID,
                auditLog.Entity,
                auditLog.Action,
                auditLog.Timestamp,
                auditLog.Details
            );
        }
    }
}
