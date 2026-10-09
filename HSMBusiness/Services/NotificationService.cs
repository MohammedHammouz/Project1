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
    public class NotificationService
    {
        ResultPatern resultPattern = new ResultPatern();
        public enum enMode { Add = 0, Update }
        public enMode _mode = enMode.Add;
        private readonly NotificationRepository _notifiction;
        private readonly PatientService _patientService;
        private readonly UserService _userService;
        public NotificationRepository notifictionRepository
        {
            get { return _notifiction; }
        }

        public NotificationService(NotificationRepository notifiction,PatientService patientService,UserService userService,enMode mode = enMode.Add)
        {
            _notifiction = notifiction;
            _patientService = patientService;
            _userService = userService;
            _mode = mode;
        }

        private async Task<(int Status,bool IsSuccess)> Add(NotificationDTO notifictionDTO)
        {
            if (!await _patientService.IsPatientExist(notifictionDTO.PatientID))
            {
                return (500, false);
            }
            if(! await _userService.IsUserExist(notifictionDTO.UserID))
            {
                return (500, false);
            }
           
            var notifictionEntity = new NotificationMapper().ToEntity(notifictionDTO);
            if (notifictionEntity.Status != "Pending" && notifictionEntity.Status != "Failed" && notifictionEntity.Status != "Sent")
            {
                return (500, false);
            }
            var AddNew =await _notifiction.AddAsync(notifictionEntity);
           
            notifictionEntity.ID = AddNew.ID;
            return (200,notifictionEntity.ID != "");
        }

        private async Task<(int, string?, bool)> Update(string ID,NotificationDTO notifictionDTO)
        {
            var CurrentNotifiction =await _notifiction.GetByIDAsync(ID);
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (!await _patientService.IsPatientExist(notifictionDTO.PatientID))
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            if (!await _userService.IsUserExist(notifictionDTO.UserID))
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            
            if (notifictionDTO.Status!="Pending" && notifictionDTO.Status != "Failed" && notifictionDTO.Status != "Sent")
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            if (CurrentNotifiction == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess);
            }
            
            CurrentNotifiction =new NotificationMapper().ToEntity(notifictionDTO,NotificationMapper.enMode.Update,CurrentNotifiction);
            bool isUpdated = await _notifiction.UpdateAsync(CurrentNotifiction);
            return (response.Status,response.Response, isUpdated);
        }

        public async Task<(int Status, string?Response, bool IsSuccess)> Save(NotificationDTO notifictionDTO,string ID)
        {
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            switch (_mode)
            {
                case enMode.Add:
                    var AddNew = await Add(notifictionDTO);
                    if (AddNew.IsSuccess)
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response =await resultPattern.GiveResponse(AddNew.Status, new Dictionary<string, string>());
                    }
                    return (response.Status,response.Response,response.IsSuccess);
                case enMode.Update:
                    return await Update(ID, notifictionDTO);
            }

            response =await resultPattern.GiveResponse(500, new Dictionary<string, string>());

            return (response.Status, response.Response,response.IsSuccess);
        }

        public async Task<(int Status,string? Response,bool IsSuccess,List<NotificationDTO>? listOfNotifications)> GetAll()
        {
            var notifictions =await _notifiction.GetAllAsync();
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            List<NotificationDTO> listOfNotifications = notifictions.Select(n => new NotificationMapper().ToDTO(n)).ToList();
            if (listOfNotifications.Count==0)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess,null);
            }
            
            return (response.Status,response.Response,response.IsSuccess, listOfNotifications);
        }

        public async Task<(int Status,string?Response,bool IsSuccess,NotificationDTO? notifictionDTO1)> GetByID(string ID)
        {
            var notifiction =await _notifiction.GetByIDAsync(ID);
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            NotificationDTO? notifictionDTO = null;
            if (notifiction == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess,notifictionDTO);
            }
            return (response.Status,response.Response,response.IsSuccess,new NotificationMapper().ToDTO(notifiction));
        }

        public async Task<(int Status, string?Response, bool IsSuccess)> Delete(string ID)
        {
            var notifiction =await _notifiction.GetByIDAsync(ID);
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (notifiction == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess);
            }
            bool IsDeleted =await _notifiction.DeleteAsync(notifiction);
            if (!IsDeleted)
            {
                response =await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess);
            }
            return (response.Status,response.Response,response.IsSuccess);
        }
    }
}