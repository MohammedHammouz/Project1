using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class NotifictionMapper
    {
        public enum enMode { Add, Update }

        public Notifiction ToEntity(
            NotifictionDTO notifictionDTO,
            enMode mode = enMode.Add)
        {
            if (mode == enMode.Add)
            {
                return new Notifiction
                {
                    ID = notifictionDTO.ID,
                    PatientID = notifictionDTO.PatientID,
                    UserID = notifictionDTO.UserID,
                    Type = notifictionDTO.Type,
                    Message = notifictionDTO.Message,
                    Status = notifictionDTO.Status,
                    SentOn = notifictionDTO.SentOn,
                    DeliveryConfirmation = notifictionDTO.DeliveryConfirmation
                };
            }
            else
            {
                return new Notifiction
                {
                    ID = notifictionDTO.ID,
                    PatientID = notifictionDTO.PatientID,
                    UserID = notifictionDTO.UserID,
                    Type = notifictionDTO.Type,
                    Message = notifictionDTO.Message,
                    Status = notifictionDTO.Status,
                    SentOn = notifictionDTO.SentOn,
                    DeliveryConfirmation = notifictionDTO.DeliveryConfirmation
                };
            }
        }

        public NotifictionDTO ToDTO(Notifiction notifiction)
        {
            return new NotifictionDTO
            (
                notifiction.ID,
                notifiction.PatientID,
                notifiction.UserID,
                notifiction.Type,
                notifiction.Message,
                notifiction.Status,
                notifiction.SentOn,
                notifiction.DeliveryConfirmation
            );
        }
    }
}
