using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class NotificationMapper
    {
        public enum enMode { Add, Update }

        public Notification ToEntity(
            NotificationDTO notifictionDTO,
            enMode mode = enMode.Add,Notification notification=null)
        {
            if (mode == enMode.Add)
            {
                return new Notification
                {
                    
                    PatientID = notifictionDTO.PatientID,
                    UserID = notifictionDTO.UserID,
                    Type = notifictionDTO.Type,
                    Message = notifictionDTO.Message,
                    Status = notifictionDTO.Status,
                    SentOn = notifictionDTO.SentOn,
                    DeliveryConfirmation = notifictionDTO.DeliveryConfirmation
                };
            }



            notification.PatientID = notifictionDTO.PatientID;
            notification.UserID = notifictionDTO.UserID;
            notification.Type = notifictionDTO.Type;
            notification.Message = notifictionDTO.Message;
            notification.Status = notifictionDTO.Status;
            notification.SentOn = notifictionDTO.SentOn;
            notification.DeliveryConfirmation = notifictionDTO.DeliveryConfirmation;
                
            return notification;
        }

        public NotificationDTO ToDTO(Notification notifiction)
        {
            return new NotificationDTO
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
